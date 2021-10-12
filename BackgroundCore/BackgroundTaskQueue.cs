using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ApiWithBackgroundTask.BackgroundCore
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        // Estrutura de dados para trabalhar com consumidores e produtores
        // Consumidor: Quem vai rodar a Task
        // Produtor: Quem vai criar a Task
        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

        public BackgroundTaskQueue()
        {
            // Cria uma fila sem limites de itens
            // Usar uma fila sem limites pode ser perigoso em cenário de alto uso, pois pode causa o uso extremo de
            // memoria e por consequência um OutOfMemoryException
            _queue = Channel.CreateUnbounded<Func<CancellationToken, ValueTask>>();

        }

        /// <summary>
        /// Método responsável por adicionar Tasks na fila
        /// </summary>
        /// <param name="workItem">A task que precisa ser feita</param>
        /// <exception cref="ArgumentNullException">Caso a task esteja nula</exception>
        public async ValueTask QueueBackgroundWorkItemAsync(
            Func<CancellationToken, ValueTask> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
        }

        /// <summary>
        /// Método responsável por recuperar tasks da fila
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}