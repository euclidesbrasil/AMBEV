using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Branch
    {
        public int Id { get; set; } // Identificador único da filial
        public string Name { get; set; } // Nome da Filial
        public string Location { get; set; } // Localização (cidade, estado, etc.)
    }
}
