using GameDashboard.Core.Models;
using GameDashboard.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GameDashboard.Core.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dashboard")
                       .RequireAuthorization();

        group.MapGet("/player/{roleId}", GetPlayer);
        group.MapPost("/recharge", Recharge);
        group.MapPost("/mail/send", SendMail);
        group.MapGet("/stats", GetStats);

        return app;
    }

    private static async Task<IResult> GetPlayer(string roleId, [FromServices] IGameService service)
    {
        var player = await service.GetPlayerAsync(roleId);
        return player != null ? Results.Ok(player) : Results.NotFound("角色不存在");
    }

    private static async Task<IResult> Recharge([FromBody] RechargeRequest req, [FromServices] IGameService service)
    {
        await service.RechargeAsync(req);
        return Results.Ok(new { success = true, message = "充值成功" });
    }

    private static async Task<IResult> SendMail([FromBody] SendMailRequest req, [FromServices] IGameService service)
    {
        await service.SendMailAsync(req);
        return Results.Ok(new { success = true, message = "邮件已发送" });
    }

    private static IResult GetStats()
    {
        return Results.Ok(new { online = 12345, todayRecharge = 888888, totalPlayers = 56789 });
    }
}