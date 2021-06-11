using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entities.FolderUsuario
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int IdInstituicaoDeEnsino { get; set; }
        public InstituicaoDeEnsino InstituicaoDeEnsino { get; internal set; }
    }
}
