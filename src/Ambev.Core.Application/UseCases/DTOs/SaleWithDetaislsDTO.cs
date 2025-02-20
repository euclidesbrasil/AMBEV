﻿using Ambev.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class SaleWithDetaislsDTO : SaleDTO
    {
        public string CustomerFirstName { get; set; } // Nome do Cliente (desnormalizado)
        public string CustomerLastName { get; set; } // Nome do Cliente (desnormalizado)
        public string BranchName { get; set; } // Nome da Filial (desnormalizado)
        public decimal TotalAmount { get; set; }
    }
}
