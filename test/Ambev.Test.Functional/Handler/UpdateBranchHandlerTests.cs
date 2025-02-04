using Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch;
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
    public class UpdateBranchHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly UpdateBranchHandler _handler;

        public UpdateBranchHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateBranchHandler(_unitOfWork, _branchRepository, _mapper);
        }

        [Fact]
        public async Task Handle_BranchExists_UpdatesBranchAndCommits()
        {
            // Arrange
            var request = new UpdateBranchRequest { Name = "Updated Branch", Location = "Updated Location" };
            request.UpdateBranchRequestIdContext(1);
            var branchEntity = new Branch { Id = 1, Name = "Branch1", Location = "Location1" };
            var response = new UpdateBranchResponse { Id = 1, Name = "Updated Branch", Location = "Updated Location" };
            _branchRepository.Get(request.Id, CancellationToken.None).Returns(Task.FromResult(branchEntity));
            _mapper.Map<UpdateBranchResponse>(branchEntity).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await _unitOfWork.Received(1).Commit(CancellationToken.None);
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task Handle_BranchDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var request = new UpdateBranchRequest { Name = "Updated Branch", Location = "Updated Location" };
            request.UpdateBranchRequestIdContext(1);

            _branchRepository.Get(request.Id, CancellationToken.None).Returns(Task.FromResult<Branch>(null));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Branch with ID 1 does not exist in our database", exception.Message);
        }
    }
}

