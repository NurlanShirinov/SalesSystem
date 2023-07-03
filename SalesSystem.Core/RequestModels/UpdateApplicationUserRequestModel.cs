using System.ComponentModel.DataAnnotations;

namespace SalesSystem.Core.ViewModels
{
    public class UpdateApplicationUserRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must contain minimum 3 maximum 20 characters")]
        public string? UserName { get; set; }


        [Required]
        [RegularExpression(@"994(5[015]|7[07])\d{7}", ErrorMessage = "Phone number is wrong. Example : 994000000000")]
        public string? PhoneNumber { get; set; }
    }
}
