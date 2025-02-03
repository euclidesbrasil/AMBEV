using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.CreateSale
{
    public class CreateSaleHandler :
       IRequestHandler<CreateSaleRequest, CreateSaleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(IUnitOfWork unitOfWork,
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

        public async Task<CreateSaleResponse> Handle(CreateSaleRequest request,
            CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Ambev.Core.Domain.Entities.Sale>(request);
            var user = await _userRepository.Get(request.UserId, cancellationToken);
            if(user is null)
            {
                throw new ArgumentNullException("User not found");
            }

            List<int> idsProducts = request.Items.Select(i => i.ProductId).ToList();
            var productsUsed = await _productRepository.Filter(x => idsProducts.Contains(x.Id), cancellationToken);
            List<SaleItem> itens = _mapper.Map<List<Ambev.Core.Domain.Entities.SaleItem>>(request.Items);
            sale.ClearItems();
            sale.AddItems(itens, productsUsed);
            sale.ApplyBusinessRules();

            sale.UserFirstName =user.Firstname;
            // TODO: FIX
            sale.BranchName = "Não nulo, pendente ajustar";
            _saleRepository.Create(sale);

            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<CreateSaleResponse>(sale);
        }
    }
}
