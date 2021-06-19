using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HM.Model.Model
{
    [Table("AppAccount")]
    public class AppAccount
    {
        public AppAccount()
        {
            Id = Guid.NewGuid().ToString();
            AppAccountDetails = new HashSet<AppAccountDetail>();
        }
        [Key]
        [Required, MaxLength(128)]
        public string Id { get; set; }
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
        public virtual ICollection<AppAccountDetail> AppAccountDetails { get; set; }
    }
}
