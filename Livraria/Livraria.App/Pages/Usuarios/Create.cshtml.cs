using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly Livraria. _context;

        public CreateModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["NomeInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuario.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
