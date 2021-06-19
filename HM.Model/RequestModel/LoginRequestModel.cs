using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HM.Model.RequestModel
{
    public class LoginRequestModel
    {
        [Required, MinLength(6), MaxLength(128)]
        public string Username { get; set; }
        [Required, MinLength(6), MaxLength(128)]
        public string Password { get; set; }
    }
}
