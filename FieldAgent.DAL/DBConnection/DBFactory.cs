using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL;

public class DBFactory
{
    private readonly AppMode _mode;
    public DBFactory(AppMode mode = AppMode.Test)
    {
        _mode = mode;
    }

    public AppDbContext GetDbContext()
    {
        var builder = new ConfigurationBuilder();
        //builder.AddUserSecrets<AppDbContext>();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        
        var config = builder.Build();
        string connectionString;
        if (_mode == AppMode.Test)
        {
            connectionString = config[$"ConnectionStrings:FieldAgentTest"];
        }
        else
        {
            connectionString = config["ConnectionStrings:FieldAgent"];
        }
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;

        return new AppDbContext(options);
    }
    
    public string GetConnectionString()
    {
        var builder = new ConfigurationBuilder();
        //builder.AddUserSecrets<AppDbContext>();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        
        var config = builder.Build();
        
        var connectionString = config["ConnectionStrings:FieldAgent"];

        return connectionString;
    }
}