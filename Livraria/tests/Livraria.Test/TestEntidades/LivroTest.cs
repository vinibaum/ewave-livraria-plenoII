using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;
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
    public class LivroTests
    {
        private LivroService livroService;
        private LivroValidator livroValidator;
        private Mock<ILivroRepository> mockLivroRepository;
        private Mock<IUsuarioRepository> mockUsuarioRepository;
        private Mock<IUsuarioLivroEmprestadoRepository> usuarioLivroEmprestadoRepository;

        [TestInitialize]
        public void Setup()
        {
            mockLivroRepository = new Mock<ILivroRepository>();
            mockUsuarioRepository = new Mock<IUsuarioRepository>();
            usuarioLivroEmprestadoRepository = new Mock<IUsuarioLivroEmprestadoRepository>();
            livroValidator = new LivroValidator(mockLivroRepository.Object, mockUsuarioRepository.Object, usuarioLivroEmprestadoRepository.Object);
            livroService = new LivroService(mockLivroRepository.Object, mockUsuarioRepository.Object, usuarioLivroEmprestadoRepository.Object, livroValidator);
        }


        [TestMethod]
        public async Task CriarLivroValidoAsync()
        {
            await livroService.Save(new LivroBuilder().BuildDto());
            Assert.AreEqual(true, livroValidator.Valido);
        }


        [TestMethod]
        public async Task DeveCriticaFaltaDeTituloInvalido()
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Titulo = null;
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarGeneroNaoInformado()
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Genero = null;
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarGeneroInformadoMaiorQueOPermitido()
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Genero = "".PadLeft(100000, 'A');
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);

        }


        [TestMethod]
        public async Task DeveCriticarEditoraNaoInformado()
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Editora = null;
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveCriticarEditoraInformadoMaiorQueOPermitido()
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Editora = "".PadLeft(100000, 'A');
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }


        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(3000000)]
        public async Task DeveCriticarPaginasInvalidas(int paginas)
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Paginas = paginas;
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("1234")]
        public async Task DeveCriticarDescricaoInvalida(string descricao)
        {
            var livro = new LivroBuilder().BuildDto();
            livro.Descricao = descricao;
            await livroService.Save(livro);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveNaoDevolverLivroNaoEmprestado()
        {
            var livro = new LivroBuilder().BuilEntidade();
            var usuario = new UsuarioBuilder().BuildEntidade1();
            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(usuario);
            await livroService.Devolver(livro.Id, usuario.Id);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveDevolverLivroEmprestado()
        {
            var livro = new LivroBuilder().BuilEntidade();
            livro.Emprestado = true;
            var usuario = new UsuarioBuilder().BuildEntidade1();
            var livros = new List<UsuarioLivroEmprestado>
            {
                new UsuarioLivroEmprestado { Id = 1, IdLivro = livro.Id, IdUsuario = usuario.Id }
            };
            usuario.LivrosEmprestados = livros;
            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(usuario);
            await livroService.Devolver(livro.Id, usuario.Id);
            Assert.AreEqual(true, livroValidator.Valido);
        }


        [TestMethod]
        public async Task DeveNaoEmprestarLivroEmprestado()
        {
            var livro = new LivroBuilder().BuilEntidade();
            livro.Emprestado = true;
            var usuario = new UsuarioBuilder().BuildEntidade1();
            var livros = new List<UsuarioLivroEmprestado>
            {
                new UsuarioLivroEmprestado { Id = 1, IdLivro = livro.Id, IdUsuario = usuario.Id }
            };
            usuario.LivrosEmprestados = livros;
            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(usuario);
            await livroService.Emprestar(livro.Id, usuario.Id);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveNaoEmprestarLivroReservado()
        {
            var usuario = new UsuarioBuilder().BuildEntidade1();
            var outroUsuario = new UsuarioBuilder().BuildEntidade2();

            var livro = new LivroBuilder().BuilEntidade();
            livro.Reservado = true;
            livro.UsuarioReserva = usuario;
            livro.IdUsuarioReserva = usuario.Id;

            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(outroUsuario);
            await livroService.Emprestar(livro.Id, outroUsuario.Id);
            Assert.AreEqual(false, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveEmprestarLivroReservadoParaOUsuarioDaReserva()
        {
            var usuario = new UsuarioBuilder().BuildEntidade1();

            var livro = new LivroBuilder().BuilEntidade();
            livro.Reservado = true;
            livro.UsuarioReserva = usuario;
            livro.IdUsuarioReserva = usuario.Id;

            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(usuario);
            await livroService.Emprestar(livro.Id, usuario.Id);
            Assert.AreEqual(true, livroValidator.Valido);
        }

        [TestMethod]
        public async Task DeveEmprestarLivroNaoEmprestado()
        {
            var usuario = new UsuarioBuilder().BuildEntidade1();
            var livro = new LivroBuilder().BuilEntidade();
            mockLivroRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(livro);
            mockUsuarioRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(usuario);
            await livroService.Emprestar(livro.Id, usuario.Id);
            Assert.AreEqual(true, livroValidator.Valido);
        }

    }
}
