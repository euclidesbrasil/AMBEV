using Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;
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

namespace Ambev.DeveloperEvaluation.Functional
{
    public class DeleteBranchHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly DeleteBranchHandler _handler;

        public DeleteBranchHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteBranchHandler(_unitOfWork, _branchRepository, _mapper);
        }

        [Fact]
        public async Task Handle_BranchExists_RemovesBranchAndCommits()
        {
            // Arrange
            var request = new DeleteBranchRequest(1);
            var branchEntity = new Branch { Id = 1, Name = "Branch1", Location = "Location1" };

            _branchRepository.Get(request.id, CancellationToken.None).Returns(Task.FromResult(branchEntity));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _branchRepository.Received(1).Delete(branchEntity);
            await _unitOfWork.Received(1).Commit(CancellationToken.None);
            Assert.Equal("Branch removed.", result.Message);
        }

        [Fact]
        public async Task Handle_BranchDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var request = new DeleteBranchRequest(1);

            _branchRepository.Get(request.id, CancellationToken.None).Returns(Task.FromResult<Branch>(null));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Branch with ID 1 does not exist in our database", exception.Message);
        }
    }
}

