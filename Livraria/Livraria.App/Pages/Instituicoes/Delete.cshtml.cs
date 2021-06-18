using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Infra.Data.Context;
using Livraria.Domain.Interfaces.Services;

namespace Livraria.App.Pages.Instituicoes
{
    public class DeleteModel : PageModel
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;

        public DeleteModel(IInstituicaoDeEnsinoService instituicaoDeEnsinoService)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InstituicaoDeEnsino = await Task.FromResult(_instituicaoDeEnsinoService.GetById(id.Value));

            if (InstituicaoDeEnsino != null)
            {
                await _instituicaoDeEnsinoService.Delete(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
