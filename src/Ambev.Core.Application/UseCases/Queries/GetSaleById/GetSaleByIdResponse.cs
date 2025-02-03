using Ambev.Core.Application.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetSaleById
{
    public class GetSaleByIdResponse : SaleWithDetaislsDTO
    {
        public decimal TotalAmount
        {
            get
            {
                return Items?.Sum(item => item.TotalPrice) ?? 0;
            }
        }
    }
}
