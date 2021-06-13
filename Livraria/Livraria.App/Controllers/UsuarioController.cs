using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Livraria.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBaseLivraria
    {

        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService UsuarioService, UsuarioValidator usuarioValidator) : base(usuarioValidator)
        {
            _usuarioService = UsuarioService;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return this._usuarioService.ObertTodos();
        }

        [HttpGet("{id:int}")]
        public Usuario Get(int id)
        {
            return this._usuarioService.GetById(id);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] UsuarioDto dto)
        {
            Func<Task> funcao = () => _usuarioService.Update(id, dto);
            return await ExecutarFuncaoAsync(funcao);
        }
 

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            Func<Task> funcao = () => _usuarioService.Delete(id);
            return await ExecutarFuncaoAsync(funcao);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastra([FromBody] UsuarioDto dto)
        {
            Func<Task> funcao = () => _usuarioService.Save(dto);
            return await ExecutarFuncaoAsync(funcao);
        }
    }
}
