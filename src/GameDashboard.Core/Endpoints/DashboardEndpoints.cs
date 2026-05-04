using GameDashboard.Core.Models;
using Microsoft.AspNetCore.Builder;        // IApplicationBuilder 等
using Microsoft.AspNetCore.Http;           // IResult、Results
using Microsoft.AspNetCore.Routing;        // ← 这里！IEndpointRouteBuilder

namespace GameDashboard.Core.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dashboard")
                       .RequireAuthorization(); // 需要登录授权才能访问

        group.MapGet("/player/{roleId}", GetPlayer);
        group.MapPost("/recharge", Recharge);
        group.MapPost("/mail/send", SendMail);
        group.MapGet("/stats", GetStats);

        return app;
    }

    private static IResult GetPlayer(string roleId)
    {
        var player = new PlayerInfo(roleId, "测试角色", 88, 9999999, 88888, 10);
        return Results.Ok(player);
    }

    private static IResult Recharge(RechargeRequest req)
    {
        Console.WriteLine($"【充值】{req.RoleId} {req.CurrencyType} x {req.Amount}");
        return Results.Ok(new { success = true, message = "充值成功" });
    }

    private static IResult SendMail(SendMailRequest req)
    {
        Console.WriteLine($"【发邮件】{req.RoleId} {req.Title}");
        return Results.Ok(new { success = true, message = "邮件已发送" });
    }

    private static IResult GetStats()
    {
        return Results.Ok(new { online = 12345, todayRecharge = 888888, totalPlayers = 56789 });
    }
}