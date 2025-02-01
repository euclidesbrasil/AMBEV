using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Application.UseCases.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Application.UseCases.Commands.Cart.CreateCart
{
    public class CreateCartRequest : CartBaseDTO, IRequest<CreateCartResponse>
    {
    }
}
