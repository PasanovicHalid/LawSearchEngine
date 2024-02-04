using MediatR;

namespace LawSearchEngine.Application.Common.RequestTypes
{
    internal interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
