
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SneakerSZN_DAL.Data;
using SneakerSZN_BLL.Interfaces.Repositories;
using SneakerSZN_DAL.Repositories;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Services;


namespace SneakerSZN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(
                    connectionString,
                    mySqlOptions => mySqlOptions.MigrationsAssembly("SneakerSZN_DAL")
                ));

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddScoped<ISneakerRepository, SneakerRepository>();
            builder.Services.AddScoped<ISneakerService, SneakerService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactFrontend", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapIdentityApi<IdentityUser>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowReactFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}
