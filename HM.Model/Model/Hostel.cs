using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("Hostel")]
    public class Hostel
    {
        public Hostel()
        {
            Id = Guid.NewGuid().ToString();
            Rooms = new HashSet<Room>();
        }
        [Key]
        [Required, MaxLength(128)]
        public string Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public int WardId { get; set; }
        [Required, MaxLength(200)]
        public string Address { get; set; }
        [Required, MaxLength(200)]
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
        public bool Lock { get; set; }
        [Required, MaxLength(128)]
        public string AccountId { get; set; }
        [ForeignKey("WardId")]
        public virtual Ward Ward { get; set; }
        [ForeignKey("AccountId")]
        public virtual AppAccount AppAccount { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
