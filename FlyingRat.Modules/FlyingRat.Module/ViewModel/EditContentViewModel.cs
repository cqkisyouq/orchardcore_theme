using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlyingRat.Module.ViewModel
{
    public class EditContentViewModel: IValidatableObject
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string TargetId { get; set; }
        public dynamic Shape { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult("标题不能为空", new[] { nameof(Title) });
            }
            else if (Title.Length > 30)
            {
                yield return new ValidationResult("标题最多30个字符", new[] { nameof(Title) });
            }

            if (Description?.Length > 200)
            {
                yield return new ValidationResult("摘要最多200个字符", new[] { nameof(Description) });
            }

            if (string.IsNullOrEmpty(Content))
            {
                yield return new ValidationResult("内容不能为空", new[] { nameof(Content) });
            }
            else if (Content?.Length < 30)
            {
                yield return new ValidationResult("内空最最少30个字符", new[] { nameof(Content) });
            }
            else if (Content?.Length > 1200)
            {
                yield return new ValidationResult("内容最多1200个字符", new[] { nameof(Content) });
            }
        }
    }
}
