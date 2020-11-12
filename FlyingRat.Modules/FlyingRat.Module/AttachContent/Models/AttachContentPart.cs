using OrchardCore.ContentManagement;

namespace FlyingRat.Module.AttachContent.Models
{
    public class AttachContentPart : ContentPart
    {
        public bool HideContent { get; set; }
        public string AttachContent { get; set; }
    }
}
