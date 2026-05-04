using GameDashboard.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameDashboard.UI.Pages.Player;

public class SearchModel : PageModel
{
    [BindProperty]
    public string RoleId { get; set; } = string.Empty;

    public PlayerInfo? Player { get; set; }

    public async Task OnPostAsync()
    {
        if (!string.IsNullOrEmpty(RoleId))
        {
            // 后面改成调用 API
            Player = new PlayerInfo(RoleId, "测试角色", 88, 9999999, 88888, 10);
        }
    }
}