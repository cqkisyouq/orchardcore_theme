using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlyingRat.Module.ViewModel
{
    public class PublishContentViewModel
    {
        [Required,MaxLength(30)]
        public string Title { get; set; }
        public string Cover { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required,MinLength(30),MaxLength(1200)]
        public string Content { get; set; }
        public string TargetId { get; set; }
        public dynamic Shape { get; set; }
    }
}
