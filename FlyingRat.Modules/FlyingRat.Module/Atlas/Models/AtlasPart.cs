using OrchardCore.ContentManagement;

namespace FlyingRat.Module.Atlas.Models
{
    public class AtlasPart:ContentPart
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; } = 10;
        public bool UseAtlas { get; set; }
    }
}
