using TransactionalOutboxPattern.Contract.Email;
using TransactionalOutboxPattern.Contract.OutBox;

namespace TransactionalOutboxPattern.Application.BackgroundService;

public class EmailBackgroundServices : IEmailBackgroundServices
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public EmailBackgroundServices(
        IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    public void Send()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var emailService = scope.ServiceProvider.GetRequiredService<IMailService>();
            var emailOutboxService = scope.ServiceProvider.GetRequiredService<IEmailOutbox>();
            var allOutboxResult = emailOutboxService.GetAll();
            if (allOutboxResult.Any())
            {
                foreach (var item in allOutboxResult)
                {
                    var res = emailService.Send(item.Order.Email, "Order is completed", "Your order has been saved in the database", false);
                    if (res)
                    {
                        var updateResult = emailOutboxService.Update(item).Result;
                    }
                }
            }
        }
    }
}