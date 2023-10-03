using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.CQRS;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public sealed class CreateUserAccountCommand : ICommand<UserAccount>
    {
        public CreateUserAccountCommand(string username, string password, string[] claims, string email, string firstName, string lastName)
        {
            Username = username;
            Password = password;
            Claims = claims;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string[] Claims { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public sealed class CreateUserAccountHandler : ICommandHandler<CreateUserAccountCommand, UserAccount>
        {
            IUserAccountRepository _repository;

            public CreateUserAccountHandler(IUserAccountRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<UserAccount>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
            {
                return await _repository.CreateUserAccount(request);
            }
        }
    }

}
