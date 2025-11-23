
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer;
using PresistenceLayer.Data;
using PresistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServiceLayer;

namespace TalabatDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemplyReference).Assembly);
            //builder.Services.AddAutoMapper(typeof(ServiceLayerAssemplyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            #endregion

            var app = builder.Build();

            //manual injection
            using var scope=app.Services.CreateScope();
            var seedObject=scope.ServiceProvider.GetService<IDataSeeding>();
            await seedObject.DataSeedAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
