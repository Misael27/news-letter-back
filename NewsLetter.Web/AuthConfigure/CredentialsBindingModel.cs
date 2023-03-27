using System.ComponentModel.DataAnnotations;

namespace NewsLetter.Web.AuthConfigure
{
    public class CredentialsBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
