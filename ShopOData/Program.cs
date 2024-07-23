
using Microsoft.AspNetCore.OData;
using ShopBussiness;

namespace ShopOData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped(typeof(ShopDbContext));
            // Add services to the container.

            builder.Services.AddControllers().AddOData(
                o=>o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
                .AddRouteComponents("odata",EDMModelBuilder.GetEDMModel())
                );
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

            app.UseHttpsRedirection();
            app.UseODataBatching();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
