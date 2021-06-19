using Livraria.Domain.Entities.Base;
using Livraria.Domain.Entities.FolderUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Livraria.Domain.Entities.FolderLivro
{
    public class Livro : EntityBase
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Titulo { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Genero { get; set; }

        [Display(Name = "Publicação")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Date)]
        public DateTime Publicacao { get; set; }

        [Display(Name = "Páginas")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Paginas { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Editora { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; }

        public bool Emprestado { get; set; }
        public bool Reservado { get; set; }
        public virtual Usuario UsuarioReserva { get; set; }

        [Display(Name = "Reservado pelo usuário:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
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
        public bool Reservar(Usuario usuarioReserva)
        {
            IdUsuarioReserva = usuarioReserva.Id;
            Reservado = true;
            return true;
        }
    }
}