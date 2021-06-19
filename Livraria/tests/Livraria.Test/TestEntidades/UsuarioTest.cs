using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Services;
using Livraria.test.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.test.TestEntidades
{
    [TestClass]
    public class UsuarioTests
    {

        private UsuarioService usuarioService;
        private UsuarioValidator usuarioValidator;
        private Mock<IInstituicaoDeEnsinoRepository> mockInstituicaoDeEnsinoRepository;
        private Mock<IUsuarioRepository> mockUsuarioRepository;

        [TestInitialize]
        public void Setup()
        {
            mockInstituicaoDeEnsinoRepository = new Mock<IInstituicaoDeEnsinoRepository>();
            mockUsuarioRepository = new Mock<IUsuarioRepository>();
            usuarioValidator = new UsuarioValidator(mockUsuarioRepository.Object, mockInstituicaoDeEnsinoRepository.Object);
            usuarioService = new UsuarioService(mockInstituicaoDeEnsinoRepository.Object, mockUsuarioRepository.Object, usuarioValidator);
            mockInstituicaoDeEnsinoRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new InstituicaoDeEnsinoBuilder().BuildEntidade());
        }

        [TestMethod]
        public async Task CriarUsuarioValido()
        {
            await usuarioService.Save(new UsuarioBuilder().BuildDto());
            Assert.AreEqual(true, usuarioValidator.Valido);
        }

        [DataRow(null)]
        [DataRow("")]
        [TestMethod]
        public async Task DeveCriticarNomeInvalido(string nome)
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.Nome = nome;
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarNomeMuitoGrande()
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.Nome = "".PadLeft(10000, 'A');
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [TestMethod]
        public async Task DeveCriticarEnderecoInvalido(string endereco)
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.Endereco = endereco;
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarEnderecoMuitoGrande()
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.Endereco = "".PadLeft(10000, 'A');
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [DataRow("12345678910")]
        [TestMethod]
        public async Task DeveCriticarCPFInvalido(string cpf)
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.CPF = cpf;
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [DataRow("123")]
        [TestMethod]
        public async Task DeveCriticarTelefoneInvalido(string telefone)
        {
            var usaurio = new UsuarioBuilder().BuildDto();
            usaurio.Telefone = telefone;
            await usuarioService.Save(usaurio);
            Assert.AreEqual(false, usuarioValidator.Valido);
        }
   
    }
}