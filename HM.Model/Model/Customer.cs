using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        [Required, MaxLength(128)]
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
        public string Image { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required, MaxLength(128)]
        public string RoomId { get; set; }
        [ForeignKey("WardId")]
        public virtual Ward Ward { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}
