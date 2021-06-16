using Livraria.Domain.Entities;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Services
{
    public class LivroService : ILivroService
    {

        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioLivroEmprestadoRepository _usuarioLivroEmprestadoRepository;
        private LivroValidator _livroValidator;

        public IList<string> Erros => throw new NotImplementedException();

        public LivroService(ILivroRepository livroRepository,
                            IUsuarioRepository usuarioRepository,
                            IUsuarioLivroEmprestadoRepository usuarioLivroEmprestadoRepository,
                            LivroValidator livroValidator)
        {
            _livroRepository = livroRepository;
            _usuarioLivroEmprestadoRepository = usuarioLivroEmprestadoRepository;
            _livroValidator = livroValidator;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Save(LivroDto dto)
        {
            if ( _livroValidator.ValidarSave(dto))
            {
                var livro = new Livro
                {
                    Titulo = dto.Titulo,
                    Genero = dto.Genero,
                    Publicacao = dto.Publicacao,
                    Paginas = dto.Paginas,
                    Autor = dto.Autor,
                    Editora = dto.Editora,
                    Descricao = dto.Descricao
                };
                await _livroRepository.Save(livro);
            }
        }

        public async Task Update(int id, LivroDto dto)
        {
            if ( _livroValidator.ValidarUpdate(id, dto))
            {
                var livro = _livroRepository.GetById(id);
                livro.Titulo = dto.Titulo;
                livro.Genero = dto.Genero;
                livro.Publicacao = dto.Publicacao;
                livro.Paginas = dto.Paginas;
                livro.Autor = dto.Autor;
                livro.Editora = dto.Editora;
                livro.Descricao = dto.Descricao;
                await _livroRepository.Update(livro);
            }
        }

        public async Task Emprestar(int idLivro, int idUsuario)
        {
            if ( _livroValidator.ValidarEmprestimo(idLivro, idUsuario))
            {
                var livro = _livroRepository.GetById(idLivro);
                livro.EmprestarLivro();
                var usuarioLivroEmprestado = new UsuarioLivroEmprestado
                {
                    IdLivro = idLivro,
                    IdUsuario = idUsuario,
                    DataEmprestimo = DateTime.Now
                };
                await _livroRepository.Update(livro);
                await _usuarioLivroEmprestadoRepository.Save(usuarioLivroEmprestado);
            }
        }

        public async Task Devolver(int idLivro, int idUsuario)
        {
            if ( _livroValidator.ValidarDevolucao(idLivro, idUsuario))
            {
                var livro = _livroRepository.GetById(idLivro);
                var usuario = _usuarioRepository.GetById(idUsuario);
                var usuarioLivroEmprestado = usuario.LivrosEmprestados.Where(x => x.IdLivro == livro.Id && x.IsDevolvido == false).FirstOrDefault();
                usuarioLivroEmprestado.DevolverLivro();
                livro.DevolverLivro();
                await _livroRepository.Update(livro);
                await _usuarioLivroEmprestadoRepository.Update(usuarioLivroEmprestado);
            }
        }

        public async Task Reservar(int idLivro, int idUsuario)
        {
            if ( _livroValidator.ValidarReserva(idLivro, idUsuario))
            {
                var livro = _livroRepository.GetById(idLivro);
                var usuario = _usuarioRepository.GetById(idUsuario);
                livro.Reservar(usuario);
                await _livroRepository.Update(livro);
            }
        }

        public async Task Delete(int id)
        {
            if ( _livroValidator.ValidarDelete(id))
                await _livroRepository.Delete(_livroRepository.GetById(id));
        }

        public Livro GetById(int Id)
        {
            return _livroRepository.GetById(Id);
        }

        public IEnumerable<Livro> ObertTodos()
        {
            return _livroRepository.GetAll();
        }

        public IEnumerable<Livro> ObterParaEmprestar()
        {
            return _livroRepository.GetEmprestimo();
        }

        public IEnumerable<Livro> ObertParaDevolver(int idUsuario)
        {
            return _livroRepository.GetDevolucao(idUsuario);
        }

        public IEnumerable<Livro> FiltrarTitulo(string filtro)
        {
            return _livroRepository.GetByFilter(filtro);
        }

        public void Dispose()
        {
            _livroRepository.Dispose();
        }


    }
}
