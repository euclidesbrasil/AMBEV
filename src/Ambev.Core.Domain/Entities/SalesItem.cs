using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; } // Identificador único do item
        public int SaleId { get; set; } // Relacionamento com a venda (FK)
        public int ProductId { get; set; } // Identidade Externa do Produto
        public string ProductName { get; set; } // Nome do Produto (desnormalizado)
        public int Quantity { get; set; } // Quantidade vendida
        public decimal UnitPrice { get; set; } // Preço unitário
        public decimal Discount { get; set; } // Valor do desconto aplicado
        public decimal TotalPrice { get; set; } // Valor total do item (considerando desconto)
        public bool IsCancelled { get; set; } // Indicador se o item foi cancelado
    }
}
