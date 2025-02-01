using Ambev.Core.Application.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetCartQueryId
{
    public class GetCartByIdResponse : CartBaseDTO
    {
        public int Id { get; init; }
    }
}
