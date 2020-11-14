using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlyingRat.Module.ViewModel
{
   public class CreateContentViewModel:IValidatableObject
    {
        [Required,MaxLength(30)]
        public string Title { get; set; }
        public string Cover { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(1200)]
        public string Content { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}
