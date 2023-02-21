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
}
