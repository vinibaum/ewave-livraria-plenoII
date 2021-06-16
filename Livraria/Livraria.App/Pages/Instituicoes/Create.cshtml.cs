using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Instituicoes
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
            return Page();
        }

        [BindProperty]
        public InstituicaoDeEnsino InstituicaoDeEnsino { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InstituicaoDeEnsino.Add(InstituicaoDeEnsino);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
