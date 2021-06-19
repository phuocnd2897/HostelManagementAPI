using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("Room")]
    public class Room
    {
        public Room()
        {
            Id = Guid.NewGuid().ToString();
            Customers = new HashSet<Customer>();
            RoomFees = new HashSet<RoomFee>();
        }
        [Key]
        [Required, MaxLength(128)]
        public string Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Capacity { get; set; }
        public int NumberOfCustomer { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string HostelId { get; set; }
        [ForeignKey("HostelId")]
        public virtual Hostel Hostel { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<RoomFee> RoomFees { get; set; }
    }
}
