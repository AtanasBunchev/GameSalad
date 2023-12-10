using GameSalad.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSaladTests.ViewModels.User;

public class LoginVMTests
{
    private LoginVM model;

    public LoginVMTests()
    {
        this.model = GetValidModel();
    }

    public static LoginVM GetValidModel()
    {
        return new LoginVM {
            Username = "name",
            Password = "pass"
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

}
