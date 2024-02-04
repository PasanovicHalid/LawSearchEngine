using MediatR;

namespace LawSearchEngine.Application.Common.RequestTypes
{
    internal interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}
