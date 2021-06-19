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
    public class IndexModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public IndexModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public IList<Domain.Entities.FolderLivro.Livro> Livro { get;set; }

        public async Task OnGetAsync()
        {
            Livro = await _context.Livro.Include(l => l.UsuarioReserva).ToListAsync();
        }
    }
}
