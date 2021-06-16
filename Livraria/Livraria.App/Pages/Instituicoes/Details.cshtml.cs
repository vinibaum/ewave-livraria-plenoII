using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Instituicoes
{
    public class DetailsModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public DetailsModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

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
    }
}
