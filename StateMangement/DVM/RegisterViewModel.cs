using System.ComponentModel.DataAnnotations;

namespace StateMangement.DVM
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(15)]
        [MinLength(2)]
        public string Name { get; set; }



        [Required]
        [MaxLength(150)]
        public string Address { get; set; }


        [Required(ErrorMessage ="اخل رقم التليفون")]
        [Phone]
        public string Phone { get; set; }



        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; }
    }
}
