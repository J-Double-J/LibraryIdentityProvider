﻿using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public interface IUserAccountRepository
    {
        public Task<Result<UserAccount>> AddUserAccount(UserAccount userAccount);
    }
}