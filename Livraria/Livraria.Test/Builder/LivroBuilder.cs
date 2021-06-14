using Livraria.Domain.Entities.FolderLivro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.test.Builder
{
    public class LivroBuilder
    {
        public LivroDto BuildDto()
        {
            return new LivroDto
            {
                Genero = "Romance Político",
                Descricao = "Mil Novecentos e Oitenta e Quatro, muitas vezes publicado como 1984, é um romance distópico da autoria do escritor britânico George Orwell e publicado em 1949",
                Publicacao = DateTime.Now.AddDays(-1000),
                Titulo = "1984",
                Editora = "Editora Sextante",
                Paginas = 234,
                Autor = "Geroge Orwel"
            };
        }

        public Livro BuilEntidade()
        {
            return new Livro
            {
                Id = 1,
                Genero = "Romance Político",
                Descricao = "Mil Novecentos e Oitenta e Quatro, muitas vezes publicado como 1984, é um romance distópico da autoria do escritor britânico George Orwell e publicado em 1949",
                Publicacao = DateTime.Now.AddDays(-1000),
                Titulo = "1984",
                Editora = "Editora Sextante",
                Paginas = 234,
                Autor = "Geroge Orwel",
                Emprestado = false,
                Reservado = false,
                IdUsuarioReserva = null
            };
        }
    }


}
