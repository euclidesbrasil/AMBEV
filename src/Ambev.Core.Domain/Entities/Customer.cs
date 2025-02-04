using Ambev.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Entities
{
    public class Customer: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        /// <summary>
        /// RG, CPF, CNPJ ...
        /// </summary>
        public string Identification { get; set; }

        public void Update(string first, string last, string identification)
        {
            FirstName = first;
            LastName = last;
            Identification = identification;
        }
    }
}
