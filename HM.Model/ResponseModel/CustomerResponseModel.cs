using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Model.ResponseModel
{
    public class CustomerResponseModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string IdCard { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string RoomId { get; set; }
    }
}
