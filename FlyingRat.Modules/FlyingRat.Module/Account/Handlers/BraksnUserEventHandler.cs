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

        public Task CreatedAsync(UserCreateContext context)
        {
            return Task.CompletedTask;
        }

        public Task CreatingAsync(UserCreateContext context)
        {
            return Task.CompletedTask;
        }

        public Task DeletedAsync(UserDeleteContext context)
        {
            return Task.CompletedTask;
        }

        public Task DeletingAsync(UserDeleteContext context)
        {
            return Task.CompletedTask;
        }

        public Task DisabledAsync(UserContext context)
        {
            return Task.CompletedTask;
        }

        public Task EnabledAsync(UserContext context)
        {
            return Task.CompletedTask;
        }

        public Task UpdatedAsync(UserUpdateContext context)
        {
            _extensionService.UpdateAuthorOf(context.User as User);
            return Task.CompletedTask;
        }

        public Task UpdatingAsync(UserUpdateContext context)
        {
            return Task.CompletedTask;
        }
    }
}
