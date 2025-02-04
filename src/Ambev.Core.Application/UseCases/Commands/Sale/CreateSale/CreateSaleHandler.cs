using Entities = Ambev.Core.Domain.Entities;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProducerMessage _producerMessage;
        public CreateSaleHandler(IUnitOfWork unitOfWork,
            ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IProducerMessage producerMessage
            )
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _producerMessage = producerMessage;
        }

        public async Task<CreateSaleResponse> Handle(CreateSaleRequest request,
            CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Entities.Sale>(request);
            var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);
            if(customer is null)
            {
                throw new ArgumentNullException("Customer not found");
            }

            List<int> idsProducts = request.Items.Select(i => i.ProductId).ToList();
            var productsUsed = await _productRepository.Filter(x => idsProducts.Contains(x.Id), cancellationToken);
            List<Entities.SaleItem> itens = _mapper.Map<List<Entities.SaleItem>>(request.Items);
            sale.ClearItems();
            sale.AddItems(itens, productsUsed);
            sale.ApplyBusinessRules();

            sale.CustomerFirstName =customer.FirstName;
            sale.CustomerLastName = customer.LastName;
            // TODO: FIX
            sale.BranchName = "Não nulo, pendente ajustar";
            _saleRepository.Create(sale);

            var createdSaleEvent = sale.GetSaleCreatedEvent();
            await _producerMessage.SendMessage(createdSaleEvent, "sale.created");

            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<CreateSaleResponse>(sale);
        }
    }
}
