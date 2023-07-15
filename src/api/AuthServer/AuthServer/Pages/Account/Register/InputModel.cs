using System.ComponentModel.DataAnnotations;

namespace AuthServer.Pages.Account.Register
{
    public class InputModel
    {
        [Required] 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
