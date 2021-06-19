using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HM.Model.RequestModel
{
    public class FeeRequestModel
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public double Price { get; set; }
        [Required, MaxLength(128)]
        public string Unit { get; set; }
        public int Status { get; set; }
    }
}
