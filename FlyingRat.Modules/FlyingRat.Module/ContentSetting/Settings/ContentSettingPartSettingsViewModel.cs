using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlyingRat.Module.ContentSetting.Settings
{
    public class ContentSettingPartSettingsViewModel
    {
        [BindNever]
        public ContentSettingPartSettings ContentSettingPartSettings { get; set; }
    }
}
