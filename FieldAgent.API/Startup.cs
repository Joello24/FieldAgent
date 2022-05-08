using System.Text;
using FieldAgent.Core;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication1;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:2000",
                    ValidAudience = "http://localhost:2000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                };
                services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            });
        
        services.AddCors(options =>
        {
            options.AddPolicy("MyAllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("*", "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });
        
        var fac = new DBFactory(AppMode.Live);
        
        services.AddControllers();
        // services.AddTransient<IAgencyAgentRepository>(r => new AgencyAgentRepository(fac));
        // services.AddTransient<IAgencyRepository>(r => new AgencyRepository(fac));
        services.AddTransient<IAgentRepository>(r => new AgentRepository(fac));
        services.AddTransient<IAliasRepository>(r => new AliasRepository(fac));
        //services.AddTransient<ILocationRepository>(r => new LocationRepository(fac));
        services.AddTransient<IMissionRepository>(r => new MissionRepository(fac));
        services.AddTransient<IReportsRepository>(r => new ReportsRepository(fac.GetConnectionString()));
        //services.AddTransient<IReportsRepository>(r => new ReportsRepository(fac.GetConnectionString()));
        //services.AddTransient<ISecurityClearanceRepository>(r => new SecurityClearanceRepository(fac));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        
        app.UseRouting();
        app.UseCors("MyAllowSpecificOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}