using Ambev.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class SaleBaseDTO
    {
        public string SaleNumber { get; set; } // Número da venda (ex: 20240201001)
        public DateTime SaleDate { get; set; } // Data da venda
        public int UserId { get; set; }  // Identidade Externa do Cliente
        public int BranchId { get; set; }  // Identidade Externa da Filial
        public bool IsCancelled { get; set; } // Indicador de cancelamento
        public List<SaleItemBaseDTO> Items { get; set; }

    }
}
