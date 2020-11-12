using System;
using System.Collections.Generic;
using System.Text;

namespace FlyingRat.Module.Atlas.ViewModel
{
    public class AtalsPartEditViewModel
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public bool UseAtlas { get; set; }
    }
}
