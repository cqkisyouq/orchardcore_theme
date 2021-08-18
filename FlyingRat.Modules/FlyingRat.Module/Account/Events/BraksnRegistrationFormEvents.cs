using FlyingRat.Module.Account.Models;
using Microsoft.AspNetCore.Identity;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using System;
using System.Threading.Tasks;
namespace FlyingRat.Module.Account.Events
{
    public class BraksnRegistrationFormEvents : IRegistrationFormEvents
    {
        private readonly UserManager<IUser> _userManager;
        private readonly IUpdateModelAccessor _updateModel;
        public BraksnRegistrationFormEvents(
            UserManager<IUser> userManager,
            IUpdateModelAccessor updateModel
            )
        {
            _userManager = userManager;
            _updateModel = updateModel;
        }
        public Task RegisteredAsync(IUser user)
        {
            return Task.CompletedTask;
        }

        public async Task RegistrationValidationAsync(Action<string, string> reportError)
        {
            var model = new RegisterViewModel();
            await _updateModel.ModelUpdater.TryUpdateModelAsync<RegisterViewModel>(model);
            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithEmail != null)
            {
                reportError("Email", "邮箱已存在，请更换邮箱注册");
            }
        }
    }
}
