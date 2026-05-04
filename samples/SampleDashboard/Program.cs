using GameDashboard.Core.Endpoints;
using GameDashboard.Core.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 注册 Dashboard
builder.Services.AddGameDashboard();

// 简单 Cookie 登录（测试用）
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });

var app = builder.Build();

// 中间件
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ==================== 你的 Dashboard API ====================
app.MapDashboardEndpoints();

// ==================== 临时页面 ====================
app.MapGet("/", () => Results.Redirect("/login"));

// 极简登录页（测试用）
app.MapGet("/login", () => """
    <!DOCTYPE html>
    <html>
    <head><meta charset="utf-8"><title>GameDashboard 登录</title></head>
    <body>
        <h2>GameDashboard 登录</h2>
        <form action="/login" method="post">
            <p>用户名: <input name="username" value="admin" /></p>
            <p>密码: <input name="password" value="123" type="password" /></p>
            <button type="submit">登录</button>
        </form>
    </body>
    </html>
""").DisableAntiforgery();

// 处理登录
app.MapPost("/login", (string username, string password) =>
{
    if (username == "admin" && password == "123")
    {
        var claims = new[] { new System.Security.Claims.Claim("name", username) };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);

        return Results.SignIn(principal, authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme);
    }
    return Results.BadRequest("账号或密码错误");
});

app.Run();