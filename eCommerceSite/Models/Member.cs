using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string Email { get; set; } = null!; // Required

        public string Password { get; set; } = null!; // Required

        public string? Phone { get; set; } // Optional

        public string? Username { get; set; } // Optional
    }

    public class  RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)] // 비밀번호 그 똥글똥글하게 바꿔주는 기능
        public string ConfimedPassword { get; set; }
    }
}
