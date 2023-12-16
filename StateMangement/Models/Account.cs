
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StateMangement.Models
{
    public class Account
    {
        [Key]
        public int  AccountId { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
       
        public bool RememberMe { get; set; }=false;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
