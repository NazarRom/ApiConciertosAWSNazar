using ApiConciertosAWSNazar.Models;
using ApiConciertosAWSNazar.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiConciertosAWSNazar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciertosController : ControllerBase
    {
        private RepositoryConcierto repo;

        public ConciertosController(RepositoryConcierto repo)
        {
            this.repo = repo;
        }

        //get categorias
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Categoria>>> Categorias()
        {
            return await this.repo.GetCategoriasAsync();
        }

        //get eventos
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Evento>>> Eventos()
        {
            return await this.repo.GetEventosAsync();
        }
        //find evento by categoría
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Evento>>> EventosByCategoria(int id)
        {
            return await this.repo.FindEventoByCategoria(id);
        }
        //insert evento
        [HttpPost]
        public async Task<ActionResult> InsertEvento(Evento evento)
        {
            await this.repo.InsertEventoAsync(evento.Nombre, evento.Artista, evento.IdCategoria, evento.Imagen);
            return Ok();
        }
    }
}
