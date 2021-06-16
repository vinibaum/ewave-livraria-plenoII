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
    public class IndexModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public IndexModel(Livraria.Infra.Data.Context.LivrariaContext context)
        {
            _context = context;
        }

        public IList<InstituicaoDeEnsino> InstituicaoDeEnsino { get;set; }

        public async Task OnGetAsync()
        {
            InstituicaoDeEnsino = await _context.InstituicaoDeEnsino.ToListAsync();
        }
    }
}
