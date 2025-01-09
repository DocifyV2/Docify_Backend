using Azure.Identity;
using Docify_Api.Services;
using Docify_Api.Services.Interfaces;
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
            var allowLocalDevPolicy = "_allowLocalDevPolicy";

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            var blobUri = configuration.GetSection("AZURE_BLOB_STORAGE").Value ?? string.Empty;
            var sqlString = configuration.GetSection("MSSQL_CONNECTION_STRING").Value ?? string.Empty;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: allowLocalDevPolicy,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000");
                    });
            });

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
            builder.Services.AddScoped<IReceiptsService, ReceiptsService>();

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
                app.UseCors(allowLocalDevPolicy);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
