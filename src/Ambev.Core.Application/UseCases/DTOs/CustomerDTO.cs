using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// RG, CPF, CNPJ ...
        /// </summary>
        public string Identification { get; set; }
    }
}
