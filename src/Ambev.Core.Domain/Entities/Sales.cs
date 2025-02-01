using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }  // Identificador único da venda
        public string SaleNumber { get; set; } // Número da venda (ex: 20240201001)
        public DateTime SaleDate { get; set; } // Data da venda
        public int UserId { get; set; }  // Identidade Externa do Cliente
        public string UserFirstName { get; set; } // Nome do Cliente (desnormalizado)
        public int BranchId { get; set; }  // Identidade Externa da Filial
        public string BranchName { get; set; } // Nome da Filial (desnormalizado)
        public decimal TotalAmount { get; set; } // Valor total da venda
        public bool IsCancelled { get; set; } // Indicador de cancelamento
        public List<SaleItem> Items { get; set; } // Relacionamento com itens da venda
    }
}
