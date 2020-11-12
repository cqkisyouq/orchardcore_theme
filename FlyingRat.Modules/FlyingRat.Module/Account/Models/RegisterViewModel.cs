using System;
using System.Collections.Generic;
using System.Text;

namespace FlyingRat.Module.Account.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
