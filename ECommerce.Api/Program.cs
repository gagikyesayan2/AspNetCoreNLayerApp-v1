using Api.Middleware;
using Ecommerce.Api.Extensions;
using Ecommerce.Api.Mapping;
using Ecommerce.Business.Mapping;
using Ecommerce.Data.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
       

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(BusinessMappingProfile).Assembly);
                // later if we create API profile:
                cfg.AddMaps(typeof(ApiMappingProfile).Assembly);
            });



            builder.Services.AddApplicationServices();
            builder.Services.AddRepositories();

            builder.Services.AddSwaggerWithJwt();
            builder.Services.AddJwtAuthentication(builder.Configuration);



            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
