using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameDashboard.UI.Pages;

public class IndexModel : PageModel
{
    public int OnlineCount { get; set; } = 12345;

    public void OnGet()
    {
        // 后面可以调用 API 获取真实数据
    }
}