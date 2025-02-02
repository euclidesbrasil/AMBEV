using Ambev.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Sale:BaseEntity
    {
        public int Id { get; set; }  // Identificador único da venda
        public string SaleNumber { get; set; } // Número da venda (ex: 20240201001)
        public DateTime SaleDate { get; set; } // Data da venda
        public int UserId { get; set; }  // Identidade Externa do Cliente
        public string UserFirstName { get; set; } // Nome do Cliente (desnormalizado)
        public int BranchId { get; set; }  // Identidade Externa da Filial
        public string BranchName { get; set; } // Nome da Filial (desnormalizado)
        public decimal TotalAmount { get; set; } // Valor total da venda
        public bool IsCancelled { get; set; } // Indicador de cancelamento
        public List<SaleItem> Items { get; set; } // Relacionamento com itens da venda

       
        public void Cancel()
        {
            IsCancelled = true;
            Items.ForEach(x =>
            {
                x.IsCancelled = true;
                x.TotalPrice = 0;
            });
        }

        public void Update(Sale request, User user)
        {
            SaleNumber = request.SaleNumber;
            SaleDate = request.SaleDate;
            UserId = request.UserId;
            BranchId = request.BranchId;
            TotalAmount = request.TotalAmount;
            IsCancelled = request.IsCancelled;
            UserId = user.Id;
            UserFirstName = user.Firstname;
        }

        public void UpdateUserInfo( User user)
        {
            UserId = user.Id;
            UserFirstName = user.Firstname;
        }
        public void UpdateItems(IEnumerable<SaleItem> items, IEnumerable<Product> productsUsed)
        {
            foreach (var itemDto in items)
            {
                var itemToUpdate = Items.FirstOrDefault(i => i.Id == itemDto.Id);

                if (itemToUpdate != null)
                {
                    itemToUpdate.ProductId = itemDto.ProductId;
                    itemToUpdate.ProductName = productsUsed.FirstOrDefault(p => p.Id == itemToUpdate.ProductId)?.Title;
                    itemToUpdate.Quantity = itemDto.Quantity;
                    itemToUpdate.UnitPrice = itemDto.UnitPrice;
                    itemToUpdate.Discount = itemDto.Discount;
                    itemToUpdate.TotalPrice = itemDto.TotalPrice;
                    itemToUpdate.IsCancelled = itemDto.IsCancelled;
                }
                else
                {
                    // Se o item não existir, pode ser adicionado, se necessário
                    Items.Add(new SaleItem
                    {
                        Id = itemDto.Id,
                        SaleId = this.Id,
                        ProductId = itemDto.ProductId,
                        ProductName = productsUsed.FirstOrDefault(p => p.Id == itemDto.ProductId)?.Title,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        Discount = itemDto.Discount,
                        TotalPrice = itemDto.TotalPrice,
                        IsCancelled = itemDto.IsCancelled
                    });
                }
            }
        }
    }
}
