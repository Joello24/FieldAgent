using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL;

public class DBFactory
{
    public AppDbContext GetDbContext()
    {
        var builder = new ConfigurationBuilder();
        //builder.AddUserSecrets<AppDbContext>();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        
        var config = builder.Build();
        
        var connectionString = config["ConnectionStrings:FieldAgent"];

        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;

        return new AppDbContext(options);
    }
}