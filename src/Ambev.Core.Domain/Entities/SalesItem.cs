using Ambev.Core.Domain.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class SaleItem
    {
        public SaleItem()
        {
        }

        public SaleItem(int saleId, int productId,string title, int totalQuantity, decimal price, bool isCancelled)
        {
            SaleId = saleId;
            ProductId = productId;
            ProductName = title;
            Quantity = totalQuantity;
            UnitPrice = price;
            IsCancelled = isCancelled;
        }

        public int Id { get; set; } // Identificador único do item
        public int SaleId { get; set; } // Relacionamento com a venda (FK)
        public int ProductId { get; set; } // Identidade Externa do Produto
        public string ProductName { get; set; } // Nome do Produto (desnormalizado)
        public int Quantity { get; set; } // Quantidade vendida
        public decimal UnitPrice { get; set; } // Preço unitário
        public decimal Discount { get; internal set; } // Valor do desconto aplicado
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (!IsCancelled)
                {
                    totalPrice = (UnitPrice * Quantity) - Discount;
                }
                return totalPrice;
            }
            private set { /* Necessário para o Entity Framework */ }

        } // Valor total do item (considerando desconto)
        public bool IsCancelled { get; set; } // Indicador se o item foi cancelado

        /// <summary>
        /// Calculate the discount of the product based on the business rules
        /// </summary>
        public void ApplyDiscount()
        {
            if (!IsCancelled)
            {
                if (Quantity < 4)
                {
                    SetPercentualDiscount(0);
                    return;
                }

                if (Quantity >= 4 && Quantity < 10)
                {
                    SetPercentualDiscount(0.10M);
                    return;

                }

                if (Quantity >= 10 && Quantity <= 20)
                {
                    SetPercentualDiscount(0.2M);
                    return;
                }
            }
        }

        private void SetPercentualDiscount(decimal discountPercentage)
        {
            Discount = discountPercentage > 0 ? TotalPrice * discountPercentage : 0;
            Discount = discountPercentage > 0 ? (UnitPrice * Quantity) * discountPercentage : 0;
        }

        /// <summary>
        /// Verify the quantity allowed of the same product and cancel the item if it exceeds the limit
        /// </summary>
        public void VerifyAllowedQuantity()
        {
            if (Quantity > 20)
            {
                throw new InvalidOperationException("The limit per item is 20 units.");
            }
        }

        public ItemCanceledEvent CreateItemCanceledEvent(bool isCanceledSale)
        {
            if (IsCancelled || isCanceledSale)
            {
                return new ItemCanceledEvent(this, "Item canceled");
            }
            return null;
        }
    }
}
