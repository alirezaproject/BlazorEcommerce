using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared;

public class UserRegister
{
    [Required,EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required,StringLength(100,MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password),ErrorMessage = "The password do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;

}