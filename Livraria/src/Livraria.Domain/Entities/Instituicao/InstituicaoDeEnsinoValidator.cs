using Livraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Livraria.Domain.Entities.FolderInstituicaoDeEnsino
{
    public class InstituicaoDeEnsinoValidator : ValidatorBase
    {
        private InstituicaoDeEnsinoDto _dto;

        private readonly IInstituicaoDeEnsinoRepository _instituicaoDeEnsinoRepository;

        public InstituicaoDeEnsinoValidator(IInstituicaoDeEnsinoRepository instituicaoDeEnsinoRepository)
        {
            _instituicaoDeEnsinoRepository = instituicaoDeEnsinoRepository;
        }

        public bool ValidarSave(InstituicaoDeEnsinoDto dto)
        {
            _dto = dto;
            ValidarNome();
            ValidarEndereço();
            ValidarCPNJ();
            return this.Valido;
        }

        public bool ValidarUpdate(int id, InstituicaoDeEnsinoDto dto)
        {
            if (_instituicaoDeEnsinoRepository.GetById(id) == null)
                AdicionaErro("Instituição de ensino não encontrada");
            _dto = dto;
            ValidarNome();
            ValidarEndereço();
            ValidarCPNJ();
            return this.Valido;
        }

        public bool ValidarDelete(int id)
        {
            if (_instituicaoDeEnsinoRepository.GetById(id) == null)
                AdicionaErro("Instituição de ensino não encontrada");
            return this.Valido;
        }
        private void ValidarNome()
        {
            if (string.IsNullOrEmpty(_dto.Nome))
                AdicionaErro("Nome da instituição de ensino é obrigatório");

            if (this.Valido && _dto.Nome.Length > 1000)
                AdicionaErro("Nome não pode possuir mais do que mil caracters");
        }
        public void ValidarEndereço()
        {
            if (string.IsNullOrEmpty(_dto.Endereco))
                AdicionaErro("Endereço é obrigatório");

            if (this.Valido && _dto.Endereco.Length > 1000)
                AdicionaErro("Endereço não pode possuir mais do que mil caracteres");
        }
        public void ValidarCPNJ()
        {
            if (string.IsNullOrEmpty(_dto.CNPJ))
                AdicionaErro("CNPJ é obrigatório");

            if (ListaErros.Count == 0)
            {
                if (_dto.CNPJ.Length != 14)
                    AdicionaErro("CNPJ dever possuir 14 Dígitos");
            }

            if (ListaErros.Count == 0)
            {
                if (!Regex.IsMatch(_dto.CNPJ, "^\\d+$"))
                    AdicionaErro("CNPJ dever possuir apenas números");
            }

            if (ListaErros.Count == 0)
            {
                if (!ValidarDigitoVerificadoCNPJ())
                    AdicionaErro("CNPJ inválido");
            }
        }
        private bool ValidarDigitoVerificadoCNPJ()
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            var CNPJ = _dto.CNPJ.Trim();
            if (CNPJ.Length != 14)
                return false;
            tempCnpj = CNPJ.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CNPJ.EndsWith(digito);
        }
    }
}
