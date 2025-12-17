using Api.Middleware;
using Ecommerce.Api.Extensions;
using Ecommerce.Data.Context;
using Microsoft.EntityFrameworkCore;

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
