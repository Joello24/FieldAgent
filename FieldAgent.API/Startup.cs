using FieldAgent.Core;
using FieldAgent.DAL;

namespace WebApplication1;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddTransient<IAgencyAgentRepository, AgencyAgentRepository>();
        services.AddTransient<IAgencyRepository, AgencyRepository>();
        services.AddTransient<IAgentRepository, AgentRepository>();
        services.AddTransient<IAliasRepository, AliasRepository>();
        services.AddTransient<ILocationRepository, LocationRepository>();
        services.AddTransient<IMissionRepository, MissionRepository>();
        services.AddTransient<IReportsRepository, ReportsRepository>();
        services.AddTransient<ISecurityClearanceRepository, SecurityClearanceRepository>();
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