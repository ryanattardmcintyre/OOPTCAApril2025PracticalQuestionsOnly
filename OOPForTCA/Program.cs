// See https://aka.ms/new-console-template for more information
using OOPForTCA;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;


string todayDate = DateTime.Now.ToString("yyyyMMdd");
 


var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=oopAprTCA2025DB"+todayDate+";Trusted_Connection=True;TrustServerCertificate=true"));
    })
    .Build();


using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.EnsureCreated();
 

Console.WriteLine("Database configured successfully.");


 
