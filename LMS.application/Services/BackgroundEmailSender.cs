using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LMS.Application.Mail
{
    public class BackgroundEmailSender(Channel<MailMessage> channel, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private readonly Channel<MailMessage> _channel = channel;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mailingService = scope.ServiceProvider.GetRequiredService<IMailingService>();
                mailingService.SendMail(message);
            }
        }
    }
}
