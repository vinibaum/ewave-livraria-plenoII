using Livraria.Domain.Entities;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Services
{
    public interface ILivroService : IServiceBase<Livro>
    {
        IEnumerable<Livro> FiltrarTitulo(string filtro);
        IEnumerable<Livro> ObertTodos();
        IEnumerable<Livro> ObterParaEmprestar();
        IEnumerable<Livro> ObertParaDevolver(int idUsuario);
        Livro GetById(int Id);
        Task Emprestar(int idLivro, int idUsuario);
        Task Devolver(int idLivro, int idUsuario);
        Task Reservar(int idLivro, int idUsuario);
        Task Save(LivroDto entity);
        Task Update(int id,LivroDto entity);
        Task Delete(int id);
    }
}
