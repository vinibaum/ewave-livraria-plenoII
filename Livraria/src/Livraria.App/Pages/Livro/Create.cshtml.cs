using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Infra.Data.Context;
using Livraria.Presentation.Controllers;
using Livraria.Domain.Interfaces.Services;
using Livraria.Domain.Services;

namespace Livraria.App.Pages.Livro
{
    public class CreateModel : PageModel
    {
        private readonly ILivroService _ILivroService;
        private readonly LivrariaContext _context;

        public CreateModel(ILivroService _ilivroService, LivrariaContext context)
        {
            _ILivroService = _ilivroService;
            _context = context;

        }

        public IActionResult OnGet()
        {
            ViewData["IdUsuarioReserva"] = new SelectList(_context.Usuario, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Domain.Entities.FolderLivro.Livro Livro { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var livro = new LivroDto
            {
                Titulo = Livro.Titulo,
                Genero = Livro.Genero,
                Publicacao = Livro.Publicacao,
                Paginas = Livro.Paginas,
                Autor = Livro.Autor,
                Editora = Livro.Editora,
                Descricao = Livro.Descricao,
                IdUsurarioReserva = Livro.IdUsuarioReserva
                
            };

            await _ILivroService.Save(livro);

            var erros = _ILivroService.Erros;

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
