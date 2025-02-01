using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class CartBaseDTO
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartItemBaseDTO> Products { get; set; }
    }
}
