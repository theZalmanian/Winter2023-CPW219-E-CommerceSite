using System.ComponentModel.DataAnnotations;

namespace E_CommerceSite.Models
{
    /// <summary>
    /// Represent's an individual user of the site
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The Member's unique identifier
        /// </summary>
        [Key]
        public int MemberID { get; set; }

        /// <summary>
        /// The Member's email address
        /// </summary>
        [Required]
        public string MemberEmail { get; set; }

        /// <summary>
        /// The member's chosen password
        /// </summary>
        [Required]
        public string MemberPassword { get; set; }

        /// <summary>
        /// The Member's chosen user-name
        /// </summary>
        public string MemberUsername { get; set; }

        /// <summary>
        /// The Member's phone number
        /// </summary>
        public string MemberPhone { get; set; }
    }

    /// <summary>
    /// Contains all necessary input to register a user
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set;}
    }
}
