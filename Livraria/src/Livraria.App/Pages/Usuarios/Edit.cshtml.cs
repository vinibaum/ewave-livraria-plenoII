using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Infra.Data.Context;
using Livraria.Domain.Interfaces.Services;

namespace Livraria.App.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;
        private readonly IUsuarioService _iusuarioservice;

        public EditModel(IUsuarioService iusuarioservice, LivrariaContext context)
        {
            _context = context;
            _iusuarioservice = iusuarioservice;
        }

        [BindProperty]
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
            ViewData["IdInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "CNPJ");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdInstituicaoDeEnsino"] = new SelectList(_context.InstituicaoDeEnsino, "Id", "CNPJ");
                return Page();
            }

            var usr = new UsuarioDto();
            usr.Nome = Usuario.Nome;
            usr.Endereco = Usuario.Endereco;
            usr.CPF = Usuario.CPF;
            usr.Telefone = Usuario.Telefone;
            usr.Email = Usuario.Email;
            usr.IdInstituicaoDeEnsino = Usuario.IdInstituicaoDeEnsino;

            try
            {
                await _iusuarioservice.Update(Usuario.Id, usr);

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
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(Usuario.Id))
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

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
