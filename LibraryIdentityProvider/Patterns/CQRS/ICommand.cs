using LibraryIdentityProvider.Patterns.ResultAndError;
using MediatR;

namespace LibraryIdentityProvider.Patterns.CQRS
{
    public interface ICommand : IRequest<Result>, ICommandBase
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
    {
    }

    public interface ICommandBase
    {
    }
}
