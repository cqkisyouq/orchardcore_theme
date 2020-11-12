using FlyingRat.Module.ContentSetting.Models;
using FlyingRat.Module.ContentSetting.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace FlyingRat.Module.ContentSetting.ViewModel
{
    public class ContentSettingPartViewModel
    {
        public bool HasCoverList { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public ContentSettingPart ContentSettingPart { get; set; }

        [BindNever]
        public ContentSettingPartSettings Settings { get; set; }
    }
}
