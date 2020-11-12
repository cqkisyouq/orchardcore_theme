using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyingRat.Module.Atlas.ViewModel
{
    public class AtlasPartViewModel
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public bool UseAtlas { get; set; }
        public ContentItem ContentItem { get; set; }
        public List<Media> Medias { get; set; } = new List<Media>();
        public dynamic Pager { get; set; }
    }

    public class Media
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
