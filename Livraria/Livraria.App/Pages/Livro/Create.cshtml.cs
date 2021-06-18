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
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        

        public CreateModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdUsuarioReserva"] = new SelectList(_context.Usuario, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Livraria.Domain.Entities.FolderLivro.Livro Livro { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Livro.Add(Livro);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
