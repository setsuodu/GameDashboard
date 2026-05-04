using GameDashboard.Core.Models;

namespace GameDashboard.Core.Services;

public interface IGameService
{
    Task<PlayerInfo?> GetPlayerAsync(string roleId);

    Task RechargeAsync(RechargeRequest request);

    Task SendMailAsync(SendMailRequest request);

    // 以后可以继续加
    // Task<List<MailHistory>> GetMailHistoryAsync(string roleId);
}