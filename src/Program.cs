namespace GameSalad;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if(!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // app.UseAuthentication();
        // app.UseAuthorization();
        // app.UseSession();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}
