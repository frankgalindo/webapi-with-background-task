using System;
using System.Threading;
using System.Threading.Tasks;
using ApiWithBackgroundTask.BackgroundCore;
using Microsoft.Extensions.Logging;

namespace ApiWithBackgroundTask.Services
{
    public class WorkService
    {
        private readonly ILogger<WorkService> _logger;
        
        // Objeto que lida com a fila
        private readonly IBackgroundTaskQueue _taskQueue;

        public WorkService(ILogger<WorkService> logger, IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            _taskQueue = taskQueue;
        }

        public async Task QueueWork(int id)
        {
            // Adiciona um novo "trabalho" na fila
            // Usa uma lambda function para conseguir passar o ID quem vem pela requisição
            // Nesse caso poderiamos passar uma lista de itens que precisam ser processados
            await _taskQueue.QueueBackgroundWorkItemAsync(cancellationToken => DoWork(id, cancellationToken));
        }

        // Simula uma task de 5 segundos
        // Aqui seria onde os itens seriam processados
        private async ValueTask DoWork(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Background Task {Id} is starting", id);

            _logger.LogInformation("Queued Background Task {Id} is running", id);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            _logger.LogInformation("Queued Background Task {Id} is complete", id);
        }
    }
}