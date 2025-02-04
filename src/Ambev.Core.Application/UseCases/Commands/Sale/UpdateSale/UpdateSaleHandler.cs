using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using Entities = Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Event;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IProducerMessage _producerMessage;
        public UpdateSaleHandler(IUnitOfWork unitOfWork,
            ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IBranchRepository branchRepository,
            IMapper mapper,
            IProducerMessage producerMessage
            )
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
            _producerMessage = producerMessage;
        }

        public async Task<UpdateSaleResponse> Handle(UpdateSaleRequest request,
            CancellationToken cancellationToken)
        {
            var allRequestItensIds = request.Items.Select(x => x.Id).ToList();
            var sale = await _saleRepository.GetSaleWithItemsAsync(request.Id, cancellationToken);
            var salesItensNotCanceled = sale.Items.Where(x => !x.IsCancelled).Select(x => x.Id).ToList();
            var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);
            var saleToUpdate = _mapper.Map<Entities.Sale>(request);
            var branch = await _branchRepository.Get(saleToUpdate.BranchId, cancellationToken);

            sale.VerifyIfAllItensAreMine(allRequestItensIds);
            sale.VerifyIfAlreadyCanceled();
            sale.VerifyIfChangeSomeItemAlreadyCanceled(saleToUpdate.Items);

            if (customer is null)
            {
                throw new KeyNotFoundException($"Customer with ID  {request.CustomerId} does not exist in our database");
            }

            if (branch is null)
            {
                throw new KeyNotFoundException($"Branch with ID  {request.BranchId} does not exist in our database");
            }

            sale.Update(saleToUpdate, customer);

            List<int> idsProducts = request.Items.Select(i => i.ProductId).ToList();
            var productsUsed = await _productRepository.Filter(x => idsProducts.Contains(x.Id), cancellationToken);

            List<Entities.SaleItem> itens =  _mapper.Map<List<Entities.SaleItem>>(request.Items);
            var newItems = itens.Where(x => x.Id == 0).ToList();
            var editItems = itens.Where(x => x.Id != 0).ToList();

            sale.AddItems(newItems, productsUsed);
            sale.UpdateItems(editItems, productsUsed);


            // TODO: FIX
            sale.BranchName = branch.Name;
            _saleRepository.Update(sale);

            // Disparar eventos de mensageria
            var itemCanceledEvents = sale.GetItensCanceledEventsOnUpdateEvent(salesItensNotCanceled, sale.IsCancelled);
            foreach (var itemCanceledEvent in itemCanceledEvents)
            {
                await _producerMessage.SendMessage(itemCanceledEvent, "sale.item.canceled");
            }

            var canceledSaleEvent = sale.GetSaleCanceledEvent();
            if (canceledSaleEvent is not null)
            {
                await _producerMessage.SendMessage(canceledSaleEvent, "sale.canceled");
            }

            var modifiedSaleEvent = sale.GetSaleModifiedEvent();
            await _producerMessage.SendMessage(modifiedSaleEvent, "sale.modified");

            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateSaleResponse>(sale);
        }
    }
}
