using BLLAUTOS.Services;
using BLLAUTOS.Services.Contracts;
using DAL;
using DALAUTOS;
using DALAUTOS.Contracts;

namespace AutosWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IDalAutoRepository, DalAutoRepository>();
            builder.Services.AddScoped<IServiceAutos, ServiceAuto>();

            //builder.Services.AddTransient<IDalColorRepository, DalColorRepository>();
            //builder.Services.AddScoped<IServiceColor, ServiceColor>();

            //builder.Services.AddTransient<IDalMarcasRepository, DalMarcasRepository>();
            //builder.Services.AddScoped<IServiceMarcas, ServiceMarcas>();

            //builder.Services.AddTransient<IDalModeloRepository, DalModeloRepository>();
            //builder.Services.AddScoped<IServiceModelo, ServiceModelo>();



            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}