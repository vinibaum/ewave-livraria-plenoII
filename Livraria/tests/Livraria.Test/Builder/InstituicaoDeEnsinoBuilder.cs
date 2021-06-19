using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.test.Builder
{
    public class InstituicaoDeEnsinoBuilder
    {
        public InstituicaoDeEnsino BuildEntidade()
        {
            return new InstituicaoDeEnsino
            {
                Id = 1,
                Nome = "Escola Educamais",
                Endereco = "Rua Isaac Póvoas 1130",
                CNPJ = "96689617000135"
            };
        }

        public InstituicaoDeEnsinoDto BuildDto()
        {
            return new InstituicaoDeEnsinoDto
            {
                Nome = "Escola Educamais",
                Endereco = "Rua Isaac Póvoas 1130",
                CNPJ = "96689617000135"
            };
        }
    }
}
