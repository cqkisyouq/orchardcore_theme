
using FlyingRat.Module.Account.Models;
using FlyingRat.Module.Account.Services;
using FlyingRat.Module.Account.ViewModel;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Users.Models;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Drivers
{
    public class UserProfileDisplayDriver: SectionDisplayDriver<User, UserProfile>
    {
        private readonly IAccountProfileService _extensionService;
        private readonly IClock _clock;
        public UserProfileDisplayDriver(IAccountProfileService extensionService, IClock clock)
        {
            _extensionService = extensionService;
            _clock = clock;
        }
        public override IDisplayResult Edit(UserProfile section, BuildEditorContext context)
        {
            return Initialize<UserProfileViewModel>("UserProfile_Edit", model =>
            {
                model.NickName = section.NickName;
                model.Photo = section.Photo;
                model.Birthday = section.Birthday;
                model.CreatedUtc = section.CreatedUtc;
                model.ModifiedUtc = section.ModifiedUtc;
            }).Location("Content:2");
        }

        public  override async Task<IDisplayResult> UpdateAsync(User user, UserProfile section, IUpdateModel updater, BuildEditorContext context)
        {
            var oldExtension = user.As<UserProfile>();
            if (await context.Updater.TryUpdateModelAsync(section, Prefix) 
                && !await _extensionService.ValidationNickName(section.NickName, oldExtension?.NickName))
            {
                updater.ModelState.AddModelError(nameof(UserProfileViewModel.NickName), "昵称已使用，请更换新昵称");
            }

            if (string.IsNullOrWhiteSpace(section.NickName))
            {
                section.NickName = oldExtension.NickName;
                updater.ModelState.AddModelError(nameof(UserProfileViewModel.NickName), "昵称不能为空");
            }
            if (context.IsNew) section.CreatedUtc = _clock.UtcNow;
            section.ModifiedUtc = _clock.UtcNow;
            return await EditAsync(section, context);
        }
    }
}
