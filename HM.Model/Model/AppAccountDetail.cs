using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("AppAccountDetail")]
    public class AppAccountDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
        public int WardId { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AccountId { get; set; }
        [ForeignKey("WardId")]
        public virtual Ward Ward { get; set; }
        [ForeignKey("AccountId")]
        public virtual AppAccount AppAccount { get; set; }
    }
}
