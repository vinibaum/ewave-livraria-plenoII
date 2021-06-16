using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Livraria.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBaseLivraria
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService, LivroValidator livroValidator) : base(livroValidator)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public IEnumerable<Livro> Get()
        {
            return this._livroService.ObterTodos();
        }

        [HttpGet("emprestar")]
        public IEnumerable<Livro> ObterTodosEmprestar()
        {
            return this._livroService.ObterParaEmprestar();
        }

        [HttpGet("{idUsuario:int}/devolver")]
        public IEnumerable<Livro> ObterTodosDevelover(int idUsuario)
        {
            return this._livroService.ObterParaDevolver(idUsuario);
        }

        [HttpGet("{id:int}")]
        public Livro Get(int id)
        {
            return this._livroService.GetById(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] LivroDto dto)
        {
            Func<Task> funcao = () => _livroService.Update(id, dto);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpPut("{idLivro:int}/usuario/{idUsuario:int}/emprestar")]
        public async Task<IActionResult> EmprestarLivro(int idLivro, int idUsuario)
        {
            Func<Task> funcao = () => _livroService.Emprestar(idLivro, idUsuario);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpPut("{idLivro:int}/usuario/{idUsuario:int}/devolver")]
        public async Task<IActionResult> Devolver(int idLivro, int idUsuario)
        {
            Func<Task> funcao = () => _livroService.Devolver(idLivro, idUsuario);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpPut("{idLivro:int}/usuario/{idUsuario:int}/reservar")]
        public async Task<IActionResult> ReservarLivro(int idLivro, int idUsuario)
        {
            Func<Task> funcao = () => _livroService.Reservar( idLivro,  idUsuario);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            Func<Task> funcao = () => _livroService.Delete(id);
            return await ExecutarFuncaoAsync(funcao);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastra([FromBody] LivroDto dto)
        {
            Func<Task> funcao = () => _livroService.Save(dto);
            return await ExecutarFuncaoAsync(funcao);
        }
    }
}
