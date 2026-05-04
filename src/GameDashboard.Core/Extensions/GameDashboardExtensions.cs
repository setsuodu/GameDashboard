using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace GameDashboard.Core.Extensions;

public static class GameDashboardExtensions
{
    public static IServiceCollection AddGameDashboard(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }

    public static IApplicationBuilder UseGameDashboard(this IApplicationBuilder app)
    {
        app.UseAuthorization();
        return app;
    }
}