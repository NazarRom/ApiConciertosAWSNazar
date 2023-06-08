using ApiConciertosAWSNazar.Data;
using ApiConciertosAWSNazar.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosAWSNazar.Repositories
{
    public class RepositoryConcierto
    {
        private ConciertosContext context;

        public RepositoryConcierto(ConciertosContext context)
        {
            this.context = context;
        }
        //getCategoria
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }

        //get eventos

        public async Task<List<Evento>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }

        //eventos por categoria
        public async Task<List<Evento>> FindEventoByCategoria(int id)
        {
            return await this.context.Eventos.Where(x => x.IdCategoria == id).ToListAsync();
        }

        //get max id
        private int GetMaxIdEvento()
        {
            return this.context.Eventos.Max(x => x.IdEvento) + 1;
        }

        //insert evento
        public async Task InsertEventoAsync(string nombre, string artista, int idcategoria, string imagen)
        {
            Evento evento = new Evento();
            evento.IdEvento = this.GetMaxIdEvento();
            evento.Nombre = nombre;
            evento.Artista = artista;
            evento.IdCategoria = idcategoria;
            evento.Imagen = imagen;
            await this.context.Eventos.AddAsync(evento);
            await this.context.SaveChangesAsync();
        }

        

    }
}
