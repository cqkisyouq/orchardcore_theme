using FlyingRat.Module.Account.Services;
using OrchardCore.Users.Handlers;
using OrchardCore.Users.Models;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Handlers
{
    public class BraksnUserEventHandler : IUserEventHandler
    {
        private readonly IAccountProfileService _extensionService;
        public BraksnUserEventHandler(
            IAccountProfileService extensionService
            )
        {
            _extensionService = extensionService;
        }

        public Task UpdatedAsync(UserContext context)
        {
            _extensionService.UpdateAuthorOf(context.User as User);
            return Task.CompletedTask ;
        }
    }
}
