using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Livro
{
    public class EditModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public EditModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Livraria.Domain.Entities.FolderLivro.Livro Livro { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Livro = await _context.Livro
                .Include(l => l.UsuarioReserva).FirstOrDefaultAsync(m => m.Id == id);

            if (Livro == null)
            {
                return NotFound();
            }
           ViewData["IdUsuarioReserva"] = new SelectList(_context.Usuario, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(Livro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.Id == id);
        }
    }
}
