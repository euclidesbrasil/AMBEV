using Ambev.Application.UseCases.Commands.Product.UpdateProduct;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.CancelSale
{
    public class CancelSaleHandler :
       IRequestHandler<CancelSaleRequest, CancelSaleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CancelSaleHandler(IUnitOfWork unitOfWork,
            ISaleRepository saleRepository,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CancelSaleResponse> Handle(CancelSaleRequest request,
            CancellationToken cancellationToken)
        {

            Ambev.Core.Domain.Entities.Sale sale = await _saleRepository.GetSaleWithItemsAsync(request.id, cancellationToken);
            if (sale == null)
            {
                throw new KeyNotFoundException("Not found.");
            }

            sale.Cancel();
            _saleRepository.Update(sale);
            await _unitOfWork.Commit(cancellationToken);
            // TODO: Call event cancel sale;
            return _mapper.Map<CancelSaleResponse>(sale);
        }
    }
}
