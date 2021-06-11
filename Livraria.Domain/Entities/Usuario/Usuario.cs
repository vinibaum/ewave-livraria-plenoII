using Livraria.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;

namespace Livraria.Domain.Entities.FolderUsuario
{
    public class Usuario : EntityBase
    {

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public int IdInstituicaoDeEnsino { get; set; }
        public virtual InstituicaoDeEnsino InstituicaoDeEnsino { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<UsuarioLivroEmprestado> LivrosEmprestados { get; set; } = new HashSet<UsuarioLivroEmprestado>();

    }
}
