using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;
using Livraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Entities.FolderLivro
{
    public class LivroValidator : ValidatorBase
    {

        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioLivroEmprestadoRepository _usuarioLivroEmprestado;
        private LivroDto _dto;

        public LivroValidator(ILivroRepository livroRepository, IUsuarioRepository usuarioRepository, IUsuarioLivroEmprestadoRepository usuarioLivroEmprestado)
        {
            _livroRepository = livroRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioLivroEmprestado = usuarioLivroEmprestado;
        }

        public bool ValidarSave(LivroDto dto)
        {
            _dto = dto;
             ValidarTituloLivro();
             ValidarGeneroLivro();
             ValidarAutor();
             ValidarEditora();
             ValidarPaginas();
             ValidarDescricao();
            return Valido;
        }

        public bool ValidarUpdate(int id, LivroDto dto)
        {
            if (_livroRepository.GetById(id) == null)
                AdicionaErro("Livro não encontrado");
            _dto = dto;
            ValidarTituloLivro();
            ValidarGeneroLivro();
            ValidarAutor();
            ValidarEditora();
            ValidarPaginas();
            ValidarDescricao();
            return Valido;
        }
        
        private void ValidarTituloLivro()
        {
            if (string.IsNullOrEmpty(_dto.Titulo))
                AdicionaErro("Título do Livro é obrigatório");

            if (Valido && _dto.Titulo.Length > 1000)
                AdicionaErro("Título não pode possuir mais que mil caracteres");
        }
        private void ValidarAutor()
        {
            if (string.IsNullOrEmpty(_dto.Autor))
                AdicionaErro("Autor do Livro é obrigatório");

            if (Valido && _dto.Autor.Length > 1000)
                AdicionaErro("Autor não pode possuir mais do que mil caracteres");
        }
        private void ValidarDescricao()
        {
            if (string.IsNullOrEmpty(_dto.Descricao))
                AdicionaErro("Descrição do livro não pode ser vázio");

            if (Valido && _dto.Descricao.Length <= 5)
                AdicionaErro("Descrição do livro deve ser maior do que 5 caracteres");
        }
        private void ValidarEditora()
        {
            if (string.IsNullOrEmpty(_dto.Editora))
                AdicionaErro("Editora do Livro é obrigatório");

            if (Valido && _dto.Editora.Length > 100)
                AdicionaErro("Editora do Livro não pode possuir mais do que 100 caracteres");
        }

        public bool ValidarEmprestimo(int idLivro, int idUsuario)
        {
            var livro = _livroRepository.GetById(idLivro);
            var usuario = _usuarioRepository.GetById(idUsuario);
            
            if (livro == null)
                AdicionaErro("Livro não encontrado");

            if (usuario == null)
                AdicionaErro("Usuário não encontrado");

            if (usuario != null && livro != null)
            {
                if (usuario.LivrosEmprestados.Where(x => x.IsDevolvido == false).Count() >= 2)
                    AdicionaErro("Não é possível emprestar livro para o usuário, porque ele já possui dois livros emprestados");

                if (livro.Emprestado)
                    AdicionaErro("Livro já está emprestado");

                if (livro.Reservado && livro.UsuarioReserva?.Id != usuario.Id)
                {
                    AdicionaErro($"O livro já está reservado para o usuáro  { livro.UsuarioReserva.Nome } ");
                    return false;
                }
            }
            return this.Valido;
        }

        public bool ValidarDevolucao(int idLivro, int idUsuario)
        {
            var livro = _livroRepository.GetById(idLivro);
            var usuario = _usuarioRepository.GetById(idUsuario);

            if (livro == null)
                AdicionaErro("Livro não encontrado");
            else
            {
                if (!livro.Emprestado)
                    AdicionaErro("Livro não está emprestado");
            }

            if (usuario == null)
                AdicionaErro("Usuário não encontrado");
            return this.Valido;
        }
        public bool ValidarReserva(int idLivro, int idUsuario)
        {
            var livro = _livroRepository.GetById(idLivro);
            var usuario = _usuarioRepository.GetById(idUsuario);

            if (livro == null)
                AdicionaErro("Livro não encontrado");
            else
            {
                if (livro.Emprestado)
                    AdicionaErro("O livro está emprestado");

                if (livro.Reservado)
                    AdicionaErro("O livro está reservado");
            }

            if (usuario == null)
                AdicionaErro("Usuário não encontrado");

            return this.Valido;
        }

        public bool ValidarDelete(int id)
        {
            var livro = _livroRepository.GetById(id);
            if (livro == null)
                AdicionaErro("Livro não encontrado");
            if (((List<UsuarioLivroEmprestado>)_usuarioLivroEmprestado.GetAllByLivro(id)).Count > 0)
                AdicionaErro("Não é possivel deletar, pois o livro está emprestado");
            return this.Valido;
        }

        private void ValidarPaginas()
        {
            if (_dto.Paginas <= 0)
                AdicionaErro("Quantidade de páginas é obrigatório");

            if (_dto.Paginas >= 300000)
                AdicionaErro("Quantidade de páginas não pode ser maior do que 300 mil");
        }
        private void ValidarGeneroLivro()
        {
            if (string.IsNullOrEmpty(_dto.Genero))
                AdicionaErro("Genero do Livro é obrigatório");

            if (Valido && _dto.Genero.Length > 1000)
                AdicionaErro("Gênero do Livro não é pode ser maior do que mil caracteres");
        }

    }
}
