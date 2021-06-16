using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Instituicoes
{
    public class EditModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public EditModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstituicaoDeEnsino InstituicaoDeEnsino { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InstituicaoDeEnsino = await _context.InstituicaoDeEnsino.FirstOrDefaultAsync(m => m.Id == id);

            if (InstituicaoDeEnsino == null)
            {
                return NotFound();
            }
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

            _context.Attach(InstituicaoDeEnsino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituicaoDeEnsinoExists(InstituicaoDeEnsino.Id))
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

        private bool InstituicaoDeEnsinoExists(int id)
        {
            return _context.InstituicaoDeEnsino.Any(e => e.Id == id);
        }
    }
}
