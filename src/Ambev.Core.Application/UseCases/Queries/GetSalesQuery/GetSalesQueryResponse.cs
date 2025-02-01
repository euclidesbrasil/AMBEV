using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetSaleById
{
    public sealed record GetSalesQueryResponse
    {
        public List<SaleDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
