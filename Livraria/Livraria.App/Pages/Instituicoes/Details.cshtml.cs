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
    public class DetailsModel : PageModel
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;

        public DetailsModel(IInstituicaoDeEnsinoService instituicaoDeEnsinoService)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
        }

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
    }
}
