using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Model.ResponseModel
{
    public class RoomResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Capacity { get; set; }
        public int NumberOfCustomer { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public int[] Fees { get; set; }
        public string HostelId { get; set; }
    }
}
