using Livraria.Domain.Entities.Base;
using Livraria.Domain.Entities.FolderUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entities.FolderLivro
{
    public class Livro : EntityBase
    {

        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime Publicacao { get; set; }
        public int Paginas { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Descricao { get; set; }
        public bool Emprestado { get; set; }
        public bool Reservado { get; set; }
        public virtual Usuario UsuarioReserva { get; set; }
        public int? IdUsuarioReserva { get; set; }

        public void DevolverLivro()
        {
            Reservado = false;
            IdUsuarioReserva = null;
            Emprestado = false;
        }
        public void EmprestarLivro()
        {
            Reservado = false;
            IdUsuarioReserva = null;
            Emprestado = true;
        }
        public bool Reservar(Usuario usuarioReservar)
        {
            IdUsuarioReserva = usuarioReservar.Id;
            Reservado = true;
            return true;
        }
    }
}