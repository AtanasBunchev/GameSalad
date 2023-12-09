using GameSalad.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameSaladTests.ViewModels.User;

public class SignUpVMTests
{
    private SignUpVM model;

    public SignUpVMTests()
    {
        this.model = GetValidModel();
    }

    public static SignUpVM GetValidModel()
    {
        return new SignUpVM {
            Username = "name",
            Password = "pass",
            RepeatPassword = "pass"
        };
    }

    public static SignUpVM GetInvalidModel()
    {
        return new SignUpVM {
            Username = "name",
            Password = "pass",
            RepeatPassword = "different"
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
    public void VerifyDifferentPasswordsFailsValidationTest()
    {
        model.RepeatPassword += "different";

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        Assert.False(isValid);
    }
}
