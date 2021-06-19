using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Infra.Data.Context;
using Livraria.Domain.Interfaces.Services;

namespace Livraria.App.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;
        private readonly IUsuarioService _iusuarioservice;

        public CreateModel(IUsuarioService iusuarioservice, LivrariaContext context)
        {
            _context = context;
            _iusuarioservice = iusuarioservice;
        }

        public IActionResult OnGet()
        {
            ViewData["IdInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "CNPJ");
            return Page();
        }

        [BindProperty]
        public UsuarioDto Usuario { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "CNPJ");
                return Page();
            }

            await _iusuarioservice.Save(Usuario);

            var erros = _iusuarioservice.Erros;

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                ViewData["IdInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "CNPJ");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
