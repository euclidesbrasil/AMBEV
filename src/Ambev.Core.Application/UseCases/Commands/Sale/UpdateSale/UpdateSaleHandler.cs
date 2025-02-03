using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale
{
    public class UpdateSaleHandler :
       IRequestHandler<UpdateSaleRequest, UpdateSaleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(IUnitOfWork unitOfWork,
            ISaleRepository saleRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResponse> Handle(UpdateSaleRequest request,
            CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleWithItemsAsync(request.Id, cancellationToken);
            var user = await _userRepository.Get(request.UserId, cancellationToken);
            var saleToUpdate = _mapper.Map<Ambev.Core.Domain.Entities.Sale>(request);
            sale.Update(saleToUpdate, user);

            List<int> idsProducts = request.Items.Select(i => i.ProductId).ToList();
            var productsUsed = await _productRepository.Filter(x => idsProducts.Contains(x.Id), cancellationToken);

            List<SaleItem> itens =  _mapper.Map<List<Ambev.Core.Domain.Entities.SaleItem>>(request.Items);
            sale.AddItems(itens.Where(x => x.Id == 0).ToList(), productsUsed);
            sale.UpdateItems(itens.Where(x => x.Id != 0).ToList(), productsUsed);

            sale.UserFirstName = user.Firstname;
            // TODO: FIX
            sale.BranchName = "Não nulo, pendente ajustar";
            _saleRepository.Update(sale);

            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateSaleResponse>(sale);
        }
    }
}
