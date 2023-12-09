using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSalad.ViewModels.User;

public class SignUpVM : IValidatableObject
{
    [DisplayName("Username: ")]
    [Required(ErrorMessage = "*This field is Required!")]
    public string? Username { get; set; }

    [DisplayName("Password: ")]
    [Required(ErrorMessage = "*This field is Required!")]
    public string? Password { get; set; }

    [DisplayName("Repeat Password: ")]
    [Required(ErrorMessage = "*This field is Required!")]
    public string? RepeatPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validactionContext)
    {
        if (Password != RepeatPassword)
        {
            yield return new ValidationResult(
                "Passwords don't match!",
                new[] { nameof(RepeatPassword) });
        }
    }
}
