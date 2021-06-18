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
using AutoMapper;

namespace Livraria.App.Pages.Instituicoes
{
    public class IndexModel : PageModel
    {
        private readonly IInstituicaoDeEnsinoService _instituicaoDeEnsinoService;
        public IndexModel(IInstituicaoDeEnsinoService instituicaoDeEnsinoService)
        {
            _instituicaoDeEnsinoService = instituicaoDeEnsinoService;
        }

        public IList<InstituicaoDeEnsino> InstituicaoDeEnsino { get; set; }

        public async Task OnGetAsync() 
        { 
            InstituicaoDeEnsino = await Task.FromResult(_instituicaoDeEnsinoService.ObterTodos().ToList());            
        }
    }
}
