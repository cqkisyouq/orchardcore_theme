using FlyingRat.Module.AttachContent.Models;
using FlyingRat.Module.AttachContent.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace FlyingRat.Module.AttachContent.ViewModel
{
    public class AttachContentPartViewModel
    {
        public string ShortName { get; set; }

        public bool HideContent { get; set; }
        /// <summary>
        /// liquid template
        /// </summary>
        public string AttachContent { get; set; }
        public string Html { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public AttachContentPart AttachContentPart { get; set; }

        [BindNever]
        public AttachContentPartSettings Settings { get; set; }
    }
}
