using System;
using System.ComponentModel.DataAnnotations;

namespace FlyingRat.Module.Account.ViewModel
{
    public class UserProfileViewModel
    {
        [Required]
        [StringLength(20)]
        public string NickName { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
    }
}
