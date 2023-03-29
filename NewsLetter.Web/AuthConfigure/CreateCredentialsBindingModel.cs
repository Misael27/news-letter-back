using System.ComponentModel.DataAnnotations;

namespace NewsLetter.Web.AuthConfigure
{
    public class CreateCredentialsBindingModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
