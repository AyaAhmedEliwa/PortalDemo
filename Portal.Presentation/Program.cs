using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Core.Reposatory;
using Portal.Core.Mapper;
using Portal.Infrastructure.Database;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Portal.Infrastructure.Extend;

namespace Portal.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //to can work dependency injection inside the controller


            #region Connection String Configration(to connect ApplicationContext by appsetting)

            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection"); //ApplicationConnection from appsettings.json

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion

            #region Auto Mapper

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion

            #region Services

            // Add services to the container.
            //builder.Services.AddControllersWithViews();// --> it mean that i work MVC

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }); //this service is to formate from camol to bascale 

            //Transient
            //builder.Services.AddTransient<IDepartmentRep, IDepartmentRep>();

            //Scoped
            builder.Services.AddScoped<IDepartmentRep, DepartmentRep>(); //initialization
            builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
            builder.Services.AddScoped<ICountryRep, CountryRep>();
            builder.Services.AddScoped<ICityRep, CityRep>();
            builder.Services.AddScoped<IDistrictRep, DistrictRep>();

            //SingleTone
            //builder.Services.AddSingleton<IDepartmentRep, IDepartmentRep>();

            #endregion

            #region Microsoft Identity

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                            options =>
                            {
                                options.LoginPath = new PathString("/Account/Login");
                                options.AccessDeniedPath = new PathString("/Account/Login");
                            });


            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<ApplicationContext>()
                            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);




            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationContext>();

            #endregion

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}