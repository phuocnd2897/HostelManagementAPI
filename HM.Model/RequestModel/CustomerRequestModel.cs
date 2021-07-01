using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HM.Model.RequestModel
{
    public class CustomerRequestModel
    {
        public string Id { get; set; }
        [Required, MaxLength(200)]
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(12)]
        public string IdCard { get; set; }
        public int WardId { get; set; }
        [Required, MaxLength(200)]
        public string Address { get; set; }
        [Required, MaxLength(200)]
        public IFormFile Image { get; set; }
        public int Status { get; set; }
        [Required, MaxLength(128)]
        public string RoomId { get; set; }
    }
}
