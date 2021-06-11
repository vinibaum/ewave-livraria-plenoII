using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Services
{
    public class InstituicaoDeEnsinoService : IInstituicaoDeEnsinoService
    {

        private readonly IInstituicaoDeEnsinoRepository _instituicaoDeEnsinoRepository;

        public InstituicaoDeEnsinoValidator _instituicaoValidator;
        public IList<string> Erros => throw new NotImplementedException();
        public InstituicaoDeEnsinoService(IInstituicaoDeEnsinoRepository InstituicaoDeEnsinoRepository, InstituicaoDeEnsinoValidator InstituicaoValidator)
        {
            _instituicaoDeEnsinoRepository = InstituicaoDeEnsinoRepository;
            _instituicaoValidator = InstituicaoValidator;
        }

        public async Task Save(InstituicaoDeEnsinoDto dto)
        {
            if (_instituicaoValidator.ValidarSave(dto))
            {
                var entity = new InstituicaoDeEnsino
                {
                    Nome = dto.Nome,
                    Endereco = dto.Endereco,
                    CNPJ = dto.CNPJ
                };
                await _instituicaoDeEnsinoRepository.Save(entity);
            }
        }

        public async Task Update(int id, InstituicaoDeEnsinoDto dto)
        {
            if (_instituicaoValidator.ValidarUpdate(id, dto))
            {
                var entity = _instituicaoDeEnsinoRepository.GetById(id);
                entity.Nome = dto.Nome;
                entity.Endereco = dto.Endereco;
                entity.CNPJ = dto.CNPJ;
                await _instituicaoDeEnsinoRepository.Update(entity);
            }
        }

        public async Task Delete(int id)
        {
            if (_instituicaoValidator.ValidarDelete(id))
                await _instituicaoDeEnsinoRepository.Delete(_instituicaoDeEnsinoRepository.GetById(id));
        }

        public InstituicaoDeEnsino GetById(int InstituicaoDeEnsinoId)
        {
            return _instituicaoDeEnsinoRepository.GetById(InstituicaoDeEnsinoId);
        }

        public IEnumerable<InstituicaoDeEnsino> ObertTodos()
        {
            return _instituicaoDeEnsinoRepository.GetAll();
        }
        public void Dispose()
        {
            _instituicaoDeEnsinoRepository.Dispose();
        }

    }
}
