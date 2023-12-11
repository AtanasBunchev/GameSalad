using System.Text;
using GameSalad.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        app.UseAuthentication();
        // app.UseAuthorization();
        // app.UseSession();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}
