﻿using Ambev.Core.Application.UseCases.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale
{
    public class UpdateSaleRequest : SaleDTO, IRequest<UpdateSaleResponse>
    {
        public int Id { get; set; }
        public List<UpdateSaleItemRequest> Items { get; set; }
    }

    public class UpdateSaleItemRequest : SaleItemBaseDTO
    {
        public int Id { get; set; } // Identificador único do item
        public int SaleId { get; set; } // Relacionamento com a venda (FK)
    }
}
