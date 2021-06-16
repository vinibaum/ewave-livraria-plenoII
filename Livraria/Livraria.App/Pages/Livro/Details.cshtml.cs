using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Livro
{
    public class DetailsModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public DetailsModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
