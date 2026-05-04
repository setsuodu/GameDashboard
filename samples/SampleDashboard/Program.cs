using GameDashboard.Core.Endpoints;
using GameDashboard.Core.Extensions;
using Microsoft.AspNetCore.Authentication;
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

// ==================== 登录处理 ====================
// GET 登录页
app.MapGet("/login", () => Results.Content("""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8">
        <title>GameDashboard 登录</title>
        <style>
            body { font-family: Arial, sans-serif; background: #f4f4f4; margin: 50px; }
            .login-box { max-width: 360px; margin: 100px auto; padding: 30px; background: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
            input { width: 100%; padding: 10px; margin: 10px 0; border: 1px solid #ccc; border-radius: 4px; }
            button { width: 100%; padding: 12px; background: #007bff; color: white; border: none; border-radius: 4px; font-size: 16px; }
        </style>
    </head>
    <body>
        <div class="login-box">
            <h2>🎮 GameDashboard GM后台</h2>
            <form action="/login" method="post">
                <p>用户名: <input name="username" value="admin" /></p>
                <p>密码: <input name="password" value="123" type="password" /></p>
                <button type="submit">登录</button>
            </form>
        </div>
    </body>
    </html>
""", "text/html; charset=utf-8")).DisableAntiforgery();

// POST 登录处理
app.MapPost("/login", async (HttpContext ctx) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();

    if (username == "admin" && password == "123")
    {
        var claims = new[] { new System.Security.Claims.Claim("name", username) };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);

        await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return Results.Redirect("/");
    }

    return Results.BadRequest("账号或密码错误");
}).DisableAntiforgery();

app.Run();