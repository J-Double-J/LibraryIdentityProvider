using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.ResultAndError;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIdentityProvider.Features.UserManagement
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : Controller
    {
        private readonly ISender _sender;

        public UserManagementController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] CreateUserAccountCommand command)
        {
            Result<UserAccount> result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return StatusCode(500, result.Error);
        }
    }
}
