using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Domain.Entitites
{
    public class ValidatorBase
    {
        public IList<string> ListaErros { get; set; }
        public ValidatorBase()
        {
            this.ListaErros = new List<string>();
        }

        public bool Valido { get => this.ListaErros.Count == 0; }
        public void AdicionaErro(string erro)
        {
            this.ListaErros.Add(erro);
        }
    }
}
