using GameDashboard.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GameDashboard.UI.Pages.Player;

public class SearchModel : PageModel
{
    [BindProperty]
    public string RoleId { get; set; } = string.Empty;

    public PlayerInfo? Player { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        Console.WriteLine($"【OnPost】收到 RoleId = '{RoleId}'");   // 用 Console 更明显

        if (!string.IsNullOrWhiteSpace(RoleId))
        {
            Player = new PlayerInfo(RoleId, $"测试角色_{RoleId}", 88, 9999999, 88888, 10);
        }

        return Page();
    }
}