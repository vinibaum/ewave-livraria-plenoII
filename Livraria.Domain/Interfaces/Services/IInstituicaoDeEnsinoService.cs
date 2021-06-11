using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Services
{
   public interface IInstituicaoDeEnsinoService: IServiceBase<InstituicaoDeEnsino>
    {
        IEnumerable<InstituicaoDeEnsino> ObertTodos();
        InstituicaoDeEnsino GetById(int id);
        Task Save(InstituicaoDeEnsinoDto entity);
        Task Update(int id,InstituicaoDeEnsinoDto entity);
        Task Delete(int id);
    }
}
