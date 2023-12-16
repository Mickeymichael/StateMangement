using System.ComponentModel.DataAnnotations;

namespace StateMangement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }
}
