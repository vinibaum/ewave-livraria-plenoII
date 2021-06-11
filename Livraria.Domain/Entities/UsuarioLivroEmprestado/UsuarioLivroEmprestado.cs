﻿using Livraria.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entities.FolderUsuarioLivroEmprestado
{
    public class UsuarioLivroEmprestado : EntityBase
    {
        public int IdUsuario { get; set; }
        public int IdLivro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool Devolvido { get; set; }

        public void DevolverLivro()
        {
            Devolvido = true;
            DataDevolucao = DateTime.Now;
        }
    }
}
