using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.RequestTypes
{
    internal interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
