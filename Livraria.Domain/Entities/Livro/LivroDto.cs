using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entities.FolderLivro
{
    public class LivroDto
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime Publicacao { get; set; }
        public int Paginas { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Descricao { get; set; }
        public int? IdUsurarioReserva { get; set; }
    }
}
