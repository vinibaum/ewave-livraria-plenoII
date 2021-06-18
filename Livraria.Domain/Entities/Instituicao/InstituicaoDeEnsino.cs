using Livraria.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Livraria.Domain.Entities.FolderInstituicaoDeEnsino
{
    public class InstituicaoDeEnsino : EntityBase
    {
        [Required(ErrorMessage = "Obrigatório")]
        public string Nome { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string CNPJ { get; set; }

    }
}
