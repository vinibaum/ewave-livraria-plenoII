using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.test.Builder
{
    public class UsuarioBuilder
    {
        public Usuario BuildEntidade1()
        {
            return new Usuario
            {
                Id = 1,
                Nome = "Joao",
                Endereco = "Rua das Pedras numero 0",
                CPF = "89312196014",
                IdInstituicaoDeEnsino = 1,
                Email = "Teste",
                Telefone = "6599862235"
            };
        }

        public Usuario BuildEntidade2()
        {
            return new Usuario
            {
                Id = 2,
                Nome = "Maria",
                Endereco = "Av. das Flores, 556",
                CPF = "08503826020",
                IdInstituicaoDeEnsino = 1,
                Email = "Teste",
                Telefone = "6599845621"
            };
        }

        internal UsuarioDto BuildDto()
        {
            return new UsuarioDto
            {
                Nome = "Armando",
                Endereco = "Rua Pinguind numero 51",
                CPF = "35322997040",
                IdInstituicaoDeEnsino = 1,
                Email = "Teste",
                Telefone = "6599255439"
            };
        }
    }
}
