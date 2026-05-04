using GameDashboard.Core.Endpoints;
using GameDashboard.Core.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGameDashboard();
builder.Services.AddRazorPages();           // 必须

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ==================== 核心映射 ====================
app.MapDashboardEndpoints();
app.MapRazorPages();                        // 必须

// ==================== 登录 ====================
app.MapGet("/login", () => Results.Content("""
    <!DOCTYPE html>
    <html>
    <head><meta charset="utf-8"><title>登录</title>
    <style>body{font-family:Arial;background:#f4f4f4;margin:40px;}
    .box{max-width:400px;margin:100px auto;padding:30px;background:white;border-radius:8px;box-shadow:0 4px 12px rgba(0,0,0,0.1);}
    input{width:100%;padding:10px;margin:8px 0;border:1px solid #ccc;border-radius:4px;}
    button{width:100%;padding:12px;background:#007bff;color:white;border:none;border-radius:4px;}</style>
    </head>
    <body>
        <div class="box">
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

app.MapPost("/login", async (HttpContext ctx) =>
{
    var form = await ctx.Request.ReadFormAsync();
    string username = form["username"];
    string password = form["password"];

    if (username == "admin" && password == "123")
    {
        var claims = new[] { new System.Security.Claims.Claim("name", username) };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);

        await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return Results.Redirect("/Index");   // 登录后跳 Index
    }
    return Results.BadRequest("账号或密码错误");
}).DisableAntiforgery();

// 未登录访问根目录也跳登录
app.MapGet("/", () => Results.Redirect("/login")).AllowAnonymous();

app.Run();