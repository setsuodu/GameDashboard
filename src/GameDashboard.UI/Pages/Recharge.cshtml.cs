using GameDashboard.Core.Models;
using GameDashboard.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameDashboard.UI.Pages;

public class RechargeModel : PageModel
{
    [BindProperty]
    public string RoleId { get; set; } = string.Empty;

    [BindProperty]
    public string CurrencyType { get; set; } = "Gold";

    [BindProperty]
    public long Amount { get; set; }

    [BindProperty]
    public string Reason { get; set; } = "GM后台充值";

    public string? SuccessMessage { get; set; }

    private readonly IGameService _gameService;

    public RechargeModel(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task OnPostAsync()
    {
        var request = new RechargeRequest(RoleId, CurrencyType, Amount, Reason);
        await _gameService.RechargeAsync(request);
        SuccessMessage = $"已成功为角色 {RoleId} 充值 {Amount} {CurrencyType}";
    }
}