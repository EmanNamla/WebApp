using WebApp.Core.Infrastructure;
using WebApp.Core.Application;
using WebApp.Core.API;
using WebApp.Core.Application.Helpers.Behaviours;


namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApiLayer(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => $"{type.Name}_{Guid.NewGuid()}");
            });

       

            var app = builder.Build();
      
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseMiddleware<ValidationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
