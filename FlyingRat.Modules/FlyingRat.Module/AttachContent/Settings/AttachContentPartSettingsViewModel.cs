using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlyingRat.Module.AttachContent.Settings
{
    public class AttachContentPartSettingsViewModel
    {
        public string ShortName { get; set; }

        [BindNever]
        public AttachContentPartSettings AttachContentPartSettings { get; set; }
    }
}
