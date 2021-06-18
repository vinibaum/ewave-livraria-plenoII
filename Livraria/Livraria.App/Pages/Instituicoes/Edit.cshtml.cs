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
using Livraria.Domain.Interfaces.Services;

namespace Livraria.App.Pages.Instituicoes
{
    public class EditModel : PageModel
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;
        private readonly Livraria.Infra.Data.Context.LivrariaContext _context;

        public EditModel(IInstituicaoDeEnsinoService instituicaoDeEnsinoService, LivrariaContext context)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
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

            InstituicaoDeEnsino = await Task.FromResult(_instituicaoDeEnsinoService.GetById(id.Value));

            if (InstituicaoDeEnsino == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var instDto = new InstituicaoDeEnsinoDto();
                instDto.Nome = InstituicaoDeEnsino.Nome;
                instDto.Endereco = InstituicaoDeEnsino.Endereco;
                instDto.CNPJ = InstituicaoDeEnsino.CNPJ;
                await _instituicaoDeEnsinoService.Update(id.Value, instDto);

                var erros = _instituicaoDeEnsinoService.Erros;

                if (erros.Count > 0)
                {
                    foreach (var item in erros)
                    {
                        ModelState.AddModelError(string.Empty, item);
                    }
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituicaoDeEnsinoExists(id.Value))
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
            return _instituicaoDeEnsinoService.GetById(id).Id > -1;
        }
    }
}
