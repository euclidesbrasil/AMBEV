using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class CartItemBaseDTO
    {
        public int ProductId { get; set; } // ID do produto
        public int Quantity { get; set; } // Quantidade do produto
    }
}
