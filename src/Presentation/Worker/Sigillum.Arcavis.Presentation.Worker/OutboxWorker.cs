using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.ProcessMessages;

namespace Sigillum.Arcavis.Presentation.Worker;

public class OutboxWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OutboxWorker> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(1);

    public OutboxWorker(IServiceScopeFactory scopeFactory, ILogger<OutboxWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dispatcher = scope.ServiceProvider.GetRequiredService<IAppCommandDispatcher>();
                await dispatcher.SendAsync(new ProcessMessagesCommand(), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OutboxWorker failed");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}