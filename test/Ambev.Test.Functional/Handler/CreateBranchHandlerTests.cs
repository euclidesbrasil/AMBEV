using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handler
{
    public class CreateBranchHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly CreateBranchHandler _handler;

        public CreateBranchHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateBranchHandler(_unitOfWork, _branchRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidRequest_CreatesBranchAndCommits()
        {
            // Arrange
            var request = new CreateBranchRequest { Name = "New Branch 1", Location = "New Location 1" };
            var branchEntity = new Branch { Id = 1, Name = "New Branch 2", Location = "New Location 2" };
            var response = new CreateBranchResponse { Id = 1, Name = "New Branch 1", Location = "New Location 1" };

            _mapper.Map<Branch>(request).Returns(branchEntity);
            _mapper.Map<CreateBranchResponse>(branchEntity).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _branchRepository.Received(1).Create(branchEntity);
            await _unitOfWork.Received(1).Commit(CancellationToken.None);
            Assert.Equal(response, result);
        }
    }
}

