using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Infra.Data.Context;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Presentation.Controllers;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Interfaces.Services;
using Livraria.Domain.Services;

namespace Livraria.App.Pages.Instituicoes
{
    public class CreateModel : PageModel
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;

        public CreateModel(IInstituicaoDeEnsinoService instituicaoDeEnsinoService)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InstituicaoDeEnsinoDto InstituicaoDeEnsino { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _instituicaoDeEnsinoService.Save(InstituicaoDeEnsino);

            var erros = _instituicaoDeEnsinoService.Erros;

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
