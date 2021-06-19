using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("RoomFee")]
    public class RoomFee
    {
        [Key, Column(Order = 1)]
        [Required, MaxLength(128)]
        public string RoomId { get; set; }
        [Key, Column(Order = 2)]
        public int FeeId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("FeeId")]
        public virtual Fee Fee { get; set; }
    }
}
