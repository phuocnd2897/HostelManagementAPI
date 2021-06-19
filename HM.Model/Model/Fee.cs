using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("Fee")]
    public class Fee
    {
        public Fee()
        {
            RoomFees = new HashSet<RoomFee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public double Price { get; set; }
        [Required, MaxLength(128)]
        public string Unit { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual ICollection<RoomFee> RoomFees { get; set; }
    }
}
