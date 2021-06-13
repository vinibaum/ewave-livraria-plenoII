using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstituicaoDeEnsinoController : ControllerBaseLivraria
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;
        public InstituicaoDeEnsinoController(IInstituicaoDeEnsinoService instituicaoDeEnsinoService, InstituicaoDeEnsinoValidator usuarioValidator) : base(usuarioValidator)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
        }

        [HttpGet]
        public IEnumerable<InstituicaoDeEnsino> Get()
        {
            return this._instituicaoDeEnsinoService.ObertTodos();
        }

        [HttpGet("{id:int}")]
        public InstituicaoDeEnsino Get(int id)
        {
            return this._instituicaoDeEnsinoService.GetById(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] InstituicaoDeEnsinoDto dto)
        {
            Func<Task> funcao = () => _instituicaoDeEnsinoService.Update(id, dto);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            Func<Task> funcao = () => _instituicaoDeEnsinoService.Delete(id);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastra([FromBody] InstituicaoDeEnsinoDto dto)
        {
            Func<Task> funcao = () => _instituicaoDeEnsinoService.Save(dto);
            return await ExecutarFuncaoAsync(funcao);
        }
    }
}
