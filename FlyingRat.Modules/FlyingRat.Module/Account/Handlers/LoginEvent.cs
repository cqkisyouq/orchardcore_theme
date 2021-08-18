using FlyingRat.Module.Account.Models;
using FlyingRat.Module.Account.Services;
using GraphQL;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using OrchardCore.Users.Models;
using System;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Handlers
{
    public class LoginEvent : ILoginFormEvent
    {
        private readonly IAccountProfileService _extensionService;
        private readonly UserManager<IUser> _userManager;
        public LoginEvent(
             IAccountProfileService extensionService,
             UserManager<IUser> userManager
            )
        {
            _extensionService = extensionService;
            _userManager = userManager;
        }

        public Task IsLockedOutAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task LoggedInAsync(IUser user)
        {
            //todo: 登录成功，缓存用户信息
            //var user = _userManager.FindByNameAsync(user.UserName).GetAwaiter().GetResult();
            if (user is User userItem)
            {
                _extensionService.CacheUserExtension(userItem.UserName, user.As<UserProfile>());
            }
            return Task.CompletedTask;
        }

        public Task LoggingInAsync(string userName, Action<string, string> reportError)
        {
            return Task.CompletedTask;
        }

        public Task LoggingInFailedAsync(string userName)
        {
            return Task.CompletedTask;
        }

        public Task LoggingInFailedAsync(IUser user)
        {
            return Task.CompletedTask;
        }
    }
}
