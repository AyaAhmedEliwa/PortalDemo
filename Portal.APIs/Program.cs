using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Core.Mapper;
using Portal.Core.Reposatory;
using Portal.Infrastructure.Database;

namespace Portal.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Connection String Configration(to connect ApplicationContext by appsetting)

            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection"); //ApplicationConnection from appsettings.json

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion

            #region Auto Mapper

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion

            #region Services

            //scoped

            builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();

            #endregion

            #region CORS 

            builder.Services.AddCors();

            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
            //.WithOrigins("https://localhost:7109/ , https://localhost:7109/ , https://localhost:7109/")
            .AllowAnyOrigin()   //any method : Get, Post, Put, Delete 
            .AllowAnyMethod()
            .AllowAnyHeader()); //header : XML, JSON

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}