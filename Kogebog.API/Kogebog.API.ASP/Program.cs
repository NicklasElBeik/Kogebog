using Kogebog.API.Repository.Database;
using Kogebog.API.Repository.Repositories;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.Services;
using Kogebog.API.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kogebog.API.ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Allow",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();

            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IUnitService, UnitService>();

            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();

            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IIngredientService, IngredientService>();

            builder.Services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.ApplyMigrations();
                app.SeedDatabase();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("Allow");

            app.MapControllers();

            app.Run();
        }
    }
}
