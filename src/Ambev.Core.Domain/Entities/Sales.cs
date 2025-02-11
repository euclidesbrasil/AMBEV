using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int Id { get; set; }  // Identificador único da venda
        public string SaleNumber { get; internal set; } // Número da venda (ex: 20240201001)
        public DateTime SaleDate { get; set; } // Data da venda
        public int CustomerId { get; set; }  // Identidade Externa do Cliente
        public string CustomerFirstName { get; set; } // Nome do Cliente (desnormalizado)
        public string CustomerLastName { get; set; } // Nome do Cliente (desnormalizado)
        public int BranchId { get; set; }  // Identidade Externa da Filial
        public string BranchName { get; set; } // Nome da Filial (desnormalizado)
        public bool IsCancelled { get; set; } // Indicador de cancelamento
        public List<SaleItem> Items { get; set; } // Relacionamento com itens da venda

        public decimal TotalAmount
        {
            get
            {
                return Items?.Sum(item => item.TotalPrice) ?? 0;
            }
            private set { /* Necessário para o Entity Framework */ }
        }

        public void Cancel()
        {
            IsCancelled = true;
            Items.ForEach(x =>
            {
                x.IsCancelled = true;
            });
        }

        public void Update(Sale request, Customer customer)
        {
            GenerateSaleNumber();
            SaleDate = request.SaleDate;
            BranchId = request.BranchId;
            IsCancelled = request.IsCancelled;
            CustomerId = customer.Id;
            CustomerFirstName = customer.FirstName;
            CustomerLastName = customer.LastName;
            if (IsCancelled)
            {
                Cancel();
            }
        }
        public void GenerateSaleNumber()
        {
            SaleNumber = Id.ToString("#000000000000");
        }
        public void ClearItems()
        {
            Items = new List<SaleItem>();
        }

        public void UpdateItems(IEnumerable<SaleItem> items, IEnumerable<Product> productsUsed)
        {
            foreach (var itemDto in items)
            {
                var itemToUpdate = Items.FirstOrDefault(i => i.Id == itemDto.Id);
                if (itemToUpdate is null)
                {
                    throw new ArgumentNullException($"Item {itemDto.Id} not found");
                }

                var product = productsUsed.FirstOrDefault(p => p.Id == itemDto.ProductId);

                if(product is null)
                {
                    throw new ArgumentNullException($"Product {itemDto.ProductId} not found");
                }

                itemToUpdate.ProductId = itemDto.ProductId;
                itemToUpdate.ProductName = product.Title;
                itemToUpdate.Quantity = itemDto.Quantity;
                itemToUpdate.UnitPrice = product.Price;
                itemToUpdate.Discount = itemDto.Discount;
                itemToUpdate.IsCancelled = itemDto.IsCancelled;
            }

            // Apply rules
            Items.ForEach(x =>
            {
                x.ApplyDiscount();
                x.VerifyAllowedQuantity();
            });
        }

        public void AddItems(IEnumerable<SaleItem> items, IEnumerable<Product> productsUsed)
        {
            foreach (var item in items)
            {
                var product = productsUsed.FirstOrDefault(p => p.Id == item.ProductId);
                if (product is null)
                {
                    throw new ArgumentNullException($"Product {item.ProductId} not found");
                }

                // Se o item não existir, pode ser adicionado, se necessário
                Items.Add(new SaleItem(item.SaleId, item.ProductId, product.Title, item.Quantity, product.Price, item.IsCancelled));
            }

            // Apply rules
            Items.ForEach(x =>
            {
                x.ApplyDiscount();
                x.VerifyAllowedQuantity();
            });
        }

        public void ApplyBusinessRules()
        {
            var groupedItems = Items.Where(x=>!x.IsCancelled)
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(i => i.Quantity),
                    Items = g.ToList()
                });

            foreach (var group in groupedItems)
            {
                if (group.TotalQuantity > 20)
                {
                    throw new InvalidOperationException($"The limit per item is 20 units for product {group.ProductId}.");
                }

                // Aplicar regras de desconto a cada item no grupo
                group.Items.ForEach(x =>
                {
                    x.ApplyDiscount();
                    x.VerifyAllowedQuantity();
                });
            }
        }

        public void VerifyIfAllItensAreMine(List<int> idsItensToVerify)
        {
            idsItensToVerify = idsItensToVerify ?? new List<int>();
            idsItensToVerify = idsItensToVerify.Where(x => x != 0).ToList();
            
            var itensNotMine = Items.Where(x => !idsItensToVerify.Contains(x.Id)).ToList();

            if(itensNotMine.Any())
            {
                throw new InvalidOperationException("Some itens are not from this sale");
            }
        }

        public void VerifyIfChangeSomeItemAlreadyCanceled(IEnumerable<SaleItem> itemsRequest)
        {
            foreach(var request in itemsRequest)
            {
                var baseItem = Items.FirstOrDefault(x => x.Id == request.Id);
                if(baseItem != null && baseItem.IsCancelled && !request.IsCancelled)
                {
                    throw new InvalidOperationException("It is not possible to reactivate a canceled item sale.");
                }
            }
        }

        public void VerifyIfAlreadyCanceled()
        {
            if(IsCancelled)
            {
                throw new InvalidOperationException("Sale already canceled");
            }
        }

        public IEnumerable<ItemCanceledEvent> GetItensCanceledEventsOnUpdateEvent(List<int> salesItensNotCanceled, bool isCanceledSale)
        {
            return Items
                .Where(item => item.IsCancelled && salesItensNotCanceled.Contains(item.Id))
                .Select(item => item.CreateItemCanceledEvent(isCanceledSale))
                .Where(e => e != null);
        }

        public SaleCanceledEvent GetSaleCanceledEvent()
        {
            if (IsCancelled)
            {
                return new SaleCanceledEvent(this, "Sale canceled");
            }
            return null;
        }

        public SaleModifiedEvent GetSaleModifiedEvent()
        {
            return new SaleModifiedEvent(this, "Sale modified");
        }

        public SaleCreatedEvent GetSaleCreatedEvent()
        {
            return new SaleCreatedEvent(this, "Sale created");
        }

    }
}
