using System;
using System.Collections.Generic;
using System.Text;

namespace FlyingRat.Module.Account.Models
{
    public class UserProfile
    {
        public string NickName { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
    }
}
