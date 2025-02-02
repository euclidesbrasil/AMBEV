using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class SaleItemBaseDTO
    {
        public int ProductId { get; set; } // Identidade Externa do Produto
        public int Quantity { get; set; } // Quantidade vendida
        public decimal UnitPrice { get; set; } // Preço unitário
        public decimal Discount { get; set; } // Valor do desconto aplicado
        public decimal TotalPrice { get; set; } // Valor total do item (considerando desconto)
        public bool IsCancelled { get; set; } // Indicador se o item foi cancelado
    }
}
