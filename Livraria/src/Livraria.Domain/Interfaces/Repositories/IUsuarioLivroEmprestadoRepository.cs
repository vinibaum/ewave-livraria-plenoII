using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;
using Livraria.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Interfaces.Repositories
{
    public interface IUsuarioLivroEmprestadoRepository : IRepositoryBase<UsuarioLivroEmprestado>
    {
        IEnumerable<UsuarioLivroEmprestado> GetAll();
        IEnumerable<UsuarioLivroEmprestado> GetAllByLivro(int idLivro);
        UsuarioLivroEmprestado GetById(int Id);
    }
}
