# GameDashboard

一个轻量、可复用、高安全性的 **.NET 10 游戏 GM 后台框架**，专为多游戏项目设计，支持**独立部署**和**嵌入式**两种方式。

---

## ✨ 功能特点

- **极简 API**：查询角色、充值货币、发送 GM 邮件、基础统计
- **支持两种接入方式**：
  - 独立 Web 项目（推荐高安全场景：内网 + 2FA + 跳板机）
  - 嵌入现有 ApiGateway
- **纯净架构**：Core + UI 分离，易于二次开发和替换前端
- **NuGet 打包支持**：一行命令即可在其他项目中使用
- **已支持**：Minimal API + Razor Pages（可轻松换成纯 HTML+JS）

---

## 📁 项目结构

```bash
GameDashboard/
├── src/
│   ├── GameDashboard.Core/          # 核心：API、DTO、接口
│   │   ├── Endpoints/
│   │   ├── Models/
│   │   ├── Services/ (IGameService)
│   │   └── Extensions/
│   └── GameDashboard.UI/            # 前端页面（Razor）
│       ├── Pages/
│       └── wwwroot/
├── samples/
│   └── SampleDashboard/             # 完整接入示例（独立项目）
├── nupkg/                           # 本地打包输出（不提交）
├── Directory.Build.props
└── README.md
```

## 🚀 使用方法
1. 引用包（推荐方式）
```Bash
# 在你的项目中执行
dotnet add package GameDashboard.Core
dotnet add package GameDashboard.UI
```

2. Program.cs 示例（独立项目）
```C#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGameDashboard();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapDashboardEndpoints();
app.MapRazorPages();

app.Run();
```

3. 登录（默认测试账号）

用户名：admin
密码：123


## 📦 打包发布

```Bash
dotnet pack src/GameDashboard.Core -c Release -o ./nupkg
dotnet pack src/GameDashboard.UI -c Release -o ./nupkg
```

接下来的工作（迭代计划）
短期（1-2 天）

- [ ] 完善充值和邮件页面
- [ ] 增加纯 HTML + JS 版本（供你快速修改）
- [ ] 写 GitHub Actions 自动打包发布
- [ ] 提供本地测试引用示例

中期

- [ ] 支持多区服切换
- [ ] 操作审计日志
- [ ] CDK 兑换码管理
- [ ] 权限系统（RBAC）
- [ ] 纯静态前端版本（Vite + Vue3 可选）

长期

- [ ] 发布到 nuget.org
- [ ] 文档站点 + 在线 Demo
- [ ] 支持 Blazor / HTMX 等多种 UI 方案


欢迎 PR 和 Issue！
如果你有特定需求（比如想换成纯前端、增加某个 GM 功能、支持特定游戏协议等），随时提 Issue，我会优先处理。

作者：setSuodu
仓库：https://github.com/setSuodu/GameDashboard
