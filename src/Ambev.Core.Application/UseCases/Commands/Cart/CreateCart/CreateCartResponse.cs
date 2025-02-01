using Ambev.Core.Application.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Application.UseCases.Commands.Cart.CreateCart
{
    public class CreateCartResponse : CartBaseDTO
    {
        public int Id { get; set; }
    }
}
