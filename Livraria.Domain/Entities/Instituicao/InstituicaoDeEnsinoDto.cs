using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entities.FolderInstituicaoDeEnsino
{
    public class InstituicaoDeEnsinoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
    }
}
