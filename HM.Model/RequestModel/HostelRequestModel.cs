using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Model.RequestModel
{
    public class HostelRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
        public IFormFile Avatar { get; set; }
        public int Status { get; set; }
        public bool Lock { get; set; }
    }
}
