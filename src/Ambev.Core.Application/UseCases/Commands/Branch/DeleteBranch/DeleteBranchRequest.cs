using AutoMapper;
using Ambev.Core.Application.UseCases.DTOs;

using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;

public sealed record DeleteBranchRequest(int id) : IRequest<DeleteBranchResponse>
{

}
