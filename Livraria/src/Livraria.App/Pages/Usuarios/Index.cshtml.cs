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
    public class IndexModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public IndexModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get;set; }

        public async Task OnGetAsync()
        {
            Usuario = await _context.Usuario
                .Include(u => u.InstituicaoDeEnsino).ToListAsync();
        }
    }
}
