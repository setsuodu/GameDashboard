namespace GameDashboard.Core.Models;

// 查询角色返回
public record PlayerInfo(
    string RoleId,
    string Name,
    int Level,
    long Gold,
    long Diamond,
    int VipLevel
);

// 充值请求
public record RechargeRequest(
    string RoleId,
    string CurrencyType,   // "Gold" "Diamond"
    long Amount,
    string Reason = "GM后台充值"
);

// 发送邮件请求
public record SendMailRequest(
    string RoleId,
    string Title,
    string Content,
    List<MailItem>? Items = null
);

public record MailItem(string ItemId, int Count);