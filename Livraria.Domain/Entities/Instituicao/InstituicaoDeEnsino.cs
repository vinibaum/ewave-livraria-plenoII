using Livraria.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Livraria.Domain.Entities.FolderInstituicaoDeEnsino
{
    public class InstituicaoDeEnsino : EntityBase
    {

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CNPJ { get; set; }
       
    }
}
