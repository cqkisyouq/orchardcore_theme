using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlyingRat.Module.ViewModel
{
    public class PublishContentViewModel: IValidatableObject
    {
        [Required(ErrorMessage = "标题不能为空")]
        [MaxLength(30,ErrorMessage = "标题最多30个字符")]
        public string Title { get; set; }
        public string Cover { get; set; }
        [MaxLength(200,ErrorMessage = "摘要最多200个字符")]
        public string Description { get; set; }
        [Required(ErrorMessage = "内容不能为空")]
        [MinLength(30,ErrorMessage = "内空最最少30个字符")]
        [MaxLength(1200,ErrorMessage = "内容最多1200个字符")]
        public string Content { get; set; }
        public string TargetId { get; set; }
        public dynamic Shape { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult("标题不能为空", new[] { nameof(Title) });
            }
            else if (Title.Length>30)
            {
                yield return new ValidationResult("标题最多30个字符", new[] { nameof(Title) });
            }
            
            if (Description?.Length > 200)
            {
                yield return new ValidationResult("摘要最多200个字符", new[] { nameof(Description)});
            }
            
            if (string.IsNullOrEmpty(Content))
            {
                yield return new ValidationResult("内容不能为空", new[] { nameof(Content)});
            }else if (Content?.Length < 30)
            {
                yield return new ValidationResult("内空最最少30个字符", new[] { nameof(Content)});
            }else if (Content?.Length > 1200)
            {
                yield return new ValidationResult("内容最多1200个字符", new[] { nameof(Content)});
            }
        }
    }
}
