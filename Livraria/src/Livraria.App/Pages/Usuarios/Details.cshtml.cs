using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Infra.Data.Context;

namespace Livraria.App.Pages.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public DetailsModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario = await _context.Usuario
                .Include(u => u.InstituicaoDeEnsino).FirstOrDefaultAsync(m => m.Id == id);

            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
