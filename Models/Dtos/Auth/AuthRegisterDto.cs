using System.ComponentModel.DataAnnotations;

namespace ToDoAPI_ASPNET.Models.Dtos.Auth;

public class AuthRegisterDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 100 characters.")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "FirstName must be between 2 and 100 characters.")]
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 100 characters.")]
    public string LastName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 chracaters.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string RePassword { get; init; } = string.Empty;
}
