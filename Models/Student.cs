using System.ComponentModel.DataAnnotations;

namespace Lap3.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải có từ 4 đến 100 ký tự.")]
        [Display(Name = "Tên sinh viên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [RegularExpression(@"^[\w\-\.]+@gmail\.com$", ErrorMessage = "Email phải có đuôi là gmail.com.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Mật khẩu phải bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required]
        public Branch? Branch { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        public bool IsRegular { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Điểm là bắt buộc.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0.")]
        [Display(Name = "Điểm")]
        public double Score { get; set; }
    }
}