using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        IEnumerable<Usuario> ObterTodos();
        Usuario GetById(int id);
        Task Save(UsuarioDto entity);
        Task Update(int id, UsuarioDto entity);
        Task Delete(int id);
    }
}
