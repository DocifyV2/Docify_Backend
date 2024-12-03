using Azure.Identity;
using InfrastructureEF.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PostDigitaliser.Server.Repository.Interfaces;
using PostDigitaliser.Server.Repository;

namespace Docify_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            var blobUri = configuration.GetSection("AZURE_BLOB_STORAGE").Value ?? string.Empty;
            var sqlString = configuration.GetSection("MSSQL_CONNECTION_STRING").Value ?? string.Empty;

            // Add services to the container.
            builder.Services.AddAzureClients(clientBuilder =>
            {
                // Add a client for the Blob Service
                clientBuilder
                    .AddBlobServiceClient(new Uri(blobUri))
                    .WithCredential(new DefaultAzureCredential());
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(sqlString);
            });

            builder.Services.AddScoped<IReceiptsRepository, ReceiptsRepository>();

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
