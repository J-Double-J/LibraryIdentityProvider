using LibraryIdentityProvider.Patterns.ResultAndError;
using MediatR;

namespace LibraryIdentityProvider.Patterns.CQRS
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
