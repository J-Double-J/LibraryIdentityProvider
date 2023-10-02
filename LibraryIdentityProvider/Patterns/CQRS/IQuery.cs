using LibraryIdentityProvider.Patterns.ResultAndError;
using MediatR;

namespace LibraryIdentityProvider.Patterns.CQRS
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
