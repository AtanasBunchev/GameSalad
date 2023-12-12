using System.Net;
using System.Text;
using GameSalad.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GameSalad;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<UsersDbContext>();
        using (var context = new UsersDbContext())
        {
            context.Database.Migrate();
        }

        builder.Services.AddControllersWithViews();

        var signingKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes("d418e5ebaee346698ebbbd375ba1f692"));
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "the.issuer",
            ValidAudience = "the.audience",

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });


        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        var authenticationCookieName = "access_token";
        app.Use(async (context, next) =>
        {
            var cookie = context.Request.Cookies[authenticationCookieName];
            if(cookie != null)
            {
                context.Request.Headers.Append("Authorization", "Bearer " + cookie);
            }

            await next(context);
        });
        app.UseAuthentication();

        app.UseStatusCodePages(context => {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)   
            {
                response.Redirect("/User/Login");
            }

            return Task.CompletedTask;
        });

        app.UseAuthorization();
        // app.UseSession();

        app.MapDefaultControllerRoute();


        app.Run();
    }
}
