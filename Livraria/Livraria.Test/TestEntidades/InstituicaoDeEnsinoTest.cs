using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
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
    public class InstituicaoDeEnsinoTest
    {
        private InstituicaoDeEnsinoValidator instituicaoDeEnsinoValidator;
        private InstituicaoDeEnsinoService instituicaoDeEnsinoService;
        private Mock<IInstituicaoDeEnsinoRepository> mockInstituicaoDeEnsinoRepository;

        [TestInitialize]
        public void Setup()
        {
            mockInstituicaoDeEnsinoRepository = new Mock<IInstituicaoDeEnsinoRepository>();
            instituicaoDeEnsinoValidator = new InstituicaoDeEnsinoValidator(mockInstituicaoDeEnsinoRepository.Object);
            instituicaoDeEnsinoService = new InstituicaoDeEnsinoService(mockInstituicaoDeEnsinoRepository.Object, instituicaoDeEnsinoValidator);
        }

        [TestMethod]
        public async Task CriarinstituicaoDeEnsinoValida()
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(true, instituicaoDeEnsinoValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [TestMethod]
        public async Task DeveCriticarNomeInvalido(string nome)
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            instituicaoDeEnsino.Nome = nome;
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(false, instituicaoDeEnsinoValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarNomeMuitoGrande()
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            instituicaoDeEnsino.Nome = "".PadLeft(10000, 'A');
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(false, instituicaoDeEnsinoValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [TestMethod]
        public async Task DeveCriticarEnderecoInvalido(string endereco)
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            instituicaoDeEnsino.Endereco = endereco;
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(false, instituicaoDeEnsinoValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarEnderecoMuitoGrande()
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            instituicaoDeEnsino.Endereco = "".PadLeft(10000, 'A');
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(false, instituicaoDeEnsinoValidator.Valido);
        }


        [DataRow(null)]
        [DataRow("")]
        [DataRow("12345678910")]
        [TestMethod]
        public async Task DeveCriticarCNPJInvalido(string cnpj)
        {
            var instituicaoDeEnsino = new InstituicaoDeEnsinoBuilder().BuildDto();
            instituicaoDeEnsino.CNPJ = cnpj;
            await instituicaoDeEnsinoService.Save(instituicaoDeEnsino);
            Assert.AreEqual(false, instituicaoDeEnsinoValidator.Valido);
        }

    }
}
