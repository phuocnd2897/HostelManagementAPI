using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HM.Model.RequestModel
{
    public class AppAccountRegisterRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
        public IFormFile Avatar { get; set; }
        [MaxLength(500)]
        public string Email { get; set; }
    }
}
