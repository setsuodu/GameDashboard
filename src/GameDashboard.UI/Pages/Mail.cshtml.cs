using GameDashboard.Core.Models;
using GameDashboard.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameDashboard.UI.Pages;

public class MailModel : PageModel
{
    [BindProperty]
    public string RoleId { get; set; } = string.Empty;

    [BindProperty]
    public string Title { get; set; } = string.Empty;

    [BindProperty]
    public string Content { get; set; } = string.Empty;

    public string? SuccessMessage { get; set; }

    private readonly IGameService _gameService;

    public MailModel(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task OnPostAsync()
    {
        var request = new SendMailRequest(RoleId, Title, Content);
        await _gameService.SendMailAsync(request);
        SuccessMessage = "邮件已发送！";
    }
}