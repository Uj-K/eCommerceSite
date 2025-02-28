using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

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

    public class LoginViewModel  // 그러니까 얘는 데이터 베이스 들어가거나 그런게 아니고 view! 보여지는 기능, input 을 collect 하는애니까 view model 이라고  
    {
        [Required]
        public string Email { get; set; } = null!; //  = null!; 구문은 C#의 null-forgiving 연산자(!)를 사용하여 컴파일러 경고를 피하기 위한 것입니다.
        /*이 구문은 다음과 같은 상황에서 유용합니다:
        •	속성이 반드시 초기화되어야 하지만, 생성자나 다른 초기화 메서드에서 초기화될 것임을 보장할 때.
        •	컴파일러에게 해당 속성이 null이 아님을 확신시켜 경고를 피하고자 할 때.*/

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
