using Livraria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Presentation.Controllers
{
    public class ControllerBaseLivraria : ControllerBase
    {
        private ValidatorBase _validator;

        public ControllerBaseLivraria(ValidatorBase validator)
        {
            _validator = validator;
        }
        protected async Task<IActionResult> ExecutarFuncaoAsync(Func<Task> funcao)
        {
            try
            {
                await funcao();
                return Ok(_validator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

     
    }
}
