using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IInstituicaoDeEnsinoRepository _InstituicaoDeEnsinoRepository;
        private UsuarioValidator _UsuarioValidator;

        public IList<string> Erros => _UsuarioValidator.ListaErros;

        public UsuarioService(IInstituicaoDeEnsinoRepository InstituicaoDeEnsinoRepository, IUsuarioRepository UsuarioRepository, UsuarioValidator UsuarioValidator)
        {
            _InstituicaoDeEnsinoRepository = InstituicaoDeEnsinoRepository;
            _UsuarioRepository = UsuarioRepository;
            _UsuarioValidator = UsuarioValidator;
        }

        public async Task Save(UsuarioDto dto)
        {
            if ( _UsuarioValidator.ValidarSave(dto))
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Endereco = dto.Endereco,
                    CPF = dto.CPF,
                    Telefone = dto.Telefone,
                    Email = dto.Email,
                    IdInstituicaoDeEnsino = dto.IdInstituicaoDeEnsino
                };
               await _UsuarioRepository.Save(usuario);
            }
        }

        public async Task Update(int id, UsuarioDto dto)
        {
            if ( _UsuarioValidator.ValidarUpdate(id, dto))
            {
                var usuario = _UsuarioRepository.GetById(id);
                usuario.Nome = dto.Nome;
                usuario.Endereco = dto.Endereco;
                usuario.CPF = dto.CPF;
                usuario.Telefone = dto.Telefone;
                usuario.Email = dto.Email;
                usuario.IdInstituicaoDeEnsino = dto.IdInstituicaoDeEnsino;
                await _UsuarioRepository.Update(usuario);
            }
        }

        public async Task Delete(int id)
        {
            if (_UsuarioValidator.ValidarDelete(id))
            {
                await _UsuarioRepository.Delete(_UsuarioRepository.GetById(id));
            }
        }

        public Usuario GetById(int UsuarioId)
        {
            return _UsuarioRepository.GetById(UsuarioId);
        }

        public IEnumerable<Usuario> ObertTodos()
        {
            return _UsuarioRepository.GetAll();
        }

        public void Dispose()
        {
            _UsuarioRepository.Dispose();
        }

    }
}
