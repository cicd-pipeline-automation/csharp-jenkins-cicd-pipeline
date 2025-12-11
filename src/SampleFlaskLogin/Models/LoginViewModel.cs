using System.ComponentModel.DataAnnotations;

namespace SampleFlaskLogin.Models
{
    /// <summary>
    /// Represents login credentials submitted by the user.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The username entered by the user.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        /// <summary>
        /// The password entered by the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
