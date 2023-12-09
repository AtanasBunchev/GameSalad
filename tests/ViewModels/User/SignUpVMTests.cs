using GameSalad.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        var messages = results
            .Where(r => r.MemberNames
                .Contains(nameof(model.Username)))
            .Select(r => r.ErrorMessage);
        Assert.False(isValid);
        Assert.NotEmpty(messages);
    }

    [Fact]
    public void VerifyMissingPasswordFailsValidationTest()
    {
        model.Password = null;

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        var messages = results
            .Where(r => r.MemberNames
                .Contains(nameof(model.Password)))
            .Select(r => r.ErrorMessage);
        Assert.False(isValid);
        Assert.NotEmpty(messages);
    }

    [Fact]
    public void VerifyDifferentPasswordsFailsValidationTest()
    {
        model.RepeatPassword += "different";

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator
            .TryValidateObject(model, context, results, true);
        var messages = results
            .Where(r => r.MemberNames
                .Contains(nameof(model.RepeatPassword)))
            .Select(r => r.ErrorMessage);
        Assert.False(isValid);
        Assert.NotEmpty(messages);
    }
}
