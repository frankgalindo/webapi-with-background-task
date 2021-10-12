using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiWithBackgroundTask.BackgroundCore
{
    /// <summary>
    /// Serviço em palno de fundo(Background Service) que consome a fila
    /// </summary>
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
            ILogger<QueuedHostedService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await BackgroundProcessing(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Recupera o item da fila
                // Caso a fila esteja vazia e código vai parar aqui e esperar até que uma nova Task chegue
                var workItem =
                    await _taskQueue.DequeueAsync(cancellationToken);

                try
                {
                    // Executa a task que foi recuperada da fila
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred executing {WorkItem}", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping");

            await base.StopAsync(cancellationToken);
        }
    }
}