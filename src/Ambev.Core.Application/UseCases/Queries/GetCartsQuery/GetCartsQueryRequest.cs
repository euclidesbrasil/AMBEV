﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetCartsQuery
{
    public sealed record GetCartsQueryRequest(string Filter, int Page, int Size, string Order) : IRequest<GetCartsQueryResponse>;
}
