using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class BranchBaseDTO
    {
        public string Name { get; set; } // Nome da filial
        
        public string Location { get; set; } // Endereço da filial
    }
}
