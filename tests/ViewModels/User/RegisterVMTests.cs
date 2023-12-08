using GameSalad.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameSaladTests.ViewModels.User;

public class RegisterVMTests
{
    private RegisterVM model;

    public RegisterVMTests()
    {
        this.model = new RegisterVM
        {
            Username = "name",
            Password = "pass",
            RepeatPassword = "pass"
        };
    }

    [Fact]
    public void VerifyValidationWithCorrectDataTest()
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        Assert.True(isValid);
    }

    [Fact]
    public void VerifyMissingUsernameFailsValidationTest()
    {
        model.Username = null;

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        Assert.False(isValid);
    }

    [Fact]
    public void VerifyMissingPasswordFailsValidationTest()
    {
        model.Password = null;

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        Assert.False(isValid);
    }

    [Fact]
    public void VerifyDifferentPasswordsFailValidationTest()
    {
        model.RepeatPassword += "different";

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        Assert.False(isValid);
    }
}
