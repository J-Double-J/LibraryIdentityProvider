using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using LibraryIdentityProvider.Patterns.CQRS;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public sealed class CreateUserAccountCommand : ICommand<UserAccount>
    {
        public CreateUserAccountCommand(string username, string password, string email, string firstName, string lastName)
        {
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public sealed class CreateUserAccountHandler : ICommandHandler<CreateUserAccountCommand, UserAccount>
        {
            IUserAccountRepository _accountRepository;
            IPasswordRepository _passwordRepository;
            IRoleRepository _roleRepository;

            public CreateUserAccountHandler(IUserAccountRepository accountRepository,
                                            IPasswordRepository passwordRepository,
                                            IRoleRepository roleRepository)
            {
                _accountRepository = accountRepository;
                _passwordRepository = passwordRepository;
                _roleRepository = roleRepository;
            }

            public async Task<Result<UserAccount>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
            {
                PasswordSecurityService passService = new(_passwordRepository);

                Result<Role> roleResult = await _roleRepository.GetRole("BasicUser");

                if (roleResult.IsFailure)
                {
                    return Result.Failure<UserAccount>(null, roleResult.Error!);
                }

                UserAccount account = new(Guid.NewGuid(),
                                          request.Username,
                                          new List<Role>() { roleResult.Value },
                                          request.Email,
                                          request.FirstName,
                                          request.LastName);

                Result passResult = await passService.CreateAndStorePasswordHash(account.ID, request.Password);

                if (passResult.IsFailure)
                {
                    return Result.Failure<UserAccount>(null, passResult.Error!);
                }

                return await _accountRepository.AddUserAccount(account);
            }
        }
    }

}
