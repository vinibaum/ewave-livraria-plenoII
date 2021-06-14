using Livraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Livraria.Domain.Entities.FolderUsuario
{
    public class UsuarioValidator : ValidatorBase
    {


        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInstituicaoDeEnsinoRepository _instituicaoDeEnsinoRepository;
        private UsuarioDto _dto;


        public UsuarioValidator(IUsuarioRepository usuarioRepository, IInstituicaoDeEnsinoRepository instituicaoDeEnsinoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _instituicaoDeEnsinoRepository = instituicaoDeEnsinoRepository;
        }

        public bool ValidarSave(UsuarioDto dto)
        {
            _dto = dto;
            ValidarNome();
            ValidarEndereço();
            ValidarCPF();
            ValidarTelefone();
            ValidarIntituicaoDeEnsino();
            return this.Valido;
        }

        public bool ValidarUpdate(int id, UsuarioDto dto)
        {
            _dto = dto;
            if (_usuarioRepository.GetById(id) == null)
                AdicionaErro("Usuário não encontrado");
            ValidarNome();
            ValidarEndereço();
            ValidarCPF();
            ValidarTelefone();
            ValidarIntituicaoDeEnsino();
            return this.Valido;
        }

        public bool ValidarDelete(int id)
        {
            if (_usuarioRepository.GetById(id) == null)
                AdicionaErro("Usuário não encontrado");
            return this.Valido;
        }

        private void ValidarNome()
        {
            if (string.IsNullOrEmpty(_dto.Nome))
                AdicionaErro("Nome da instituição de ensino é obrigatório");

            if (this.Valido && _dto.Nome.Length > 1000)
                AdicionaErro("Nome não pode possuir mais do que mil caracpossuirs");
        }
        private void ValidarEndereço()
        {
            if (string.IsNullOrEmpty(_dto.Endereco))
                AdicionaErro("Endereço é obrigatório");

            if (this.Valido && this._dto.Endereco.Length > 1000)
                AdicionaErro("Endereço não pode possuir mais do que mil caracpossuirs");
        }
        private void ValidarCPF()
        {
            if (string.IsNullOrEmpty(_dto.CPF))
                AdicionaErro("CPF é obrigatório");

            if (_dto.CPF?.Length != 11)
                AdicionaErro("CPF dever possuir 11 Dígitos");

            if (ListaErros.Count == 0)
            {
                if (!Regex.IsMatch(_dto.CPF, "^\\d+$"))
                    AdicionaErro("CPF dever possuir apenas números");
            }

            if (ListaErros.Count == 0)
            {
                if (!ValidarDigitoVerificadoCpf())
                    AdicionaErro("CPF inválido");
            }
        }
        private void ValidarTelefone()
        {
            if (string.IsNullOrEmpty(_dto.Telefone))
                AdicionaErro("Telefone é obrigatório");

            if (_dto.Telefone?.Length < 9)
                AdicionaErro("Telefone deve possuir 9 ou mais dígitos");
        }

        private void ValidarIntituicaoDeEnsino()
        {
            if (_instituicaoDeEnsinoRepository.GetById(_dto.IdInstituicaoDeEnsino) == null)
                AdicionaErro("Instituição de ensino não localizada");
        }
        private bool ValidarDigitoVerificadoCpf()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            var CPF = _dto.CPF.Trim();
            if (CPF.Length != 11)
                return false;
            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CPF.EndsWith(digito);
        }

    }
}
