using System.Threading.Tasks;
using ApiWithBackgroundTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithBackgroundTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackgroundController : ControllerBase
    {
        private readonly WorkService _workService;

        public BackgroundController(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Recebe novas requisições de tasks
        /// </summary>
        /// <param name="id"></param>
        [HttpPost("{id}")]
        public async Task PostWorkItem(int id)
        {
            // Chama o serviço responsável por adicionar itens na fila de trabalho
            await _workService.QueueWork(id);
        }
    }
}