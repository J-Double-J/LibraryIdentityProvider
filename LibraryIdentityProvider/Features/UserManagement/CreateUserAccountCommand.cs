using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity;
using LibraryIdentityProvider.Patterns.CQRS;
using LibraryIdentityProvider.Patterns.ResultAndError;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            IPasswordRepository _passwordRepository;

            public CreateUserAccountHandler(IUserAccountRepository repository, IPasswordRepository passwordRepository)
            {
                _repository = repository;
                _passwordRepository = passwordRepository;
            }

            public async Task<Result<UserAccount>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
            {
                PasswordSecurityService passService = new(_passwordRepository);

                UserAccount account = new(Guid.NewGuid(), request.Username, request.Claims, request.Email, request.FirstName, request.LastName);

                Result result = await passService.CreateAndStorePasswordHash(account.Id, request.Password);

                if (result.IsFailure)
                {
                    return Result.Failure<UserAccount>(null, result.Error!);
                }

                return await _repository.AddUserAccount(account);
            }
        }
    }

}
