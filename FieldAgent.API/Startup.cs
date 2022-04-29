using FieldAgent.Core;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        var fac = new DBFactory();
        
        services.AddControllers();
        services.AddTransient<IAgencyAgentRepository>(r => new AgencyAgentRepository(fac));
        services.AddTransient<IAgencyRepository>(r => new AgencyRepository(fac));
        services.AddTransient<IAgentRepository>(r => new AgentRepository(fac));
        services.AddTransient<IAliasRepository>(r => new AliasRepository(fac));
        services.AddTransient<ILocationRepository>(r => new LocationRepository(fac));
        services.AddTransient<IMissionRepository>(r => new MissionRepository(fac));
        services.AddTransient<IReportsRepository>(r => new ReportsRepository(fac.GetConnectionString()));
        services.AddTransient<ISecurityClearanceRepository>(r => new SecurityClearanceRepository(fac));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}