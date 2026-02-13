using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Web.Http;
using CDB.Api.Models;
using CDB.Api.Services;

namespace CDB.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    public class CdbController : ApiController
    {
        private readonly ICdbCalculator _calculator;

        public CdbController(ICdbCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost]
        [Route("api/cdb/calcular")]
        public IHttpActionResult Calcular([FromBody] CdbRequest request)
        {
            if (request == null)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "O corpo da requisição é obrigatório." });

            if (!ValidarModelo(request, out var mensagemErro))
                return Content(HttpStatusCode.BadRequest, new { mensagem = mensagemErro });

            var response = _calculator.Calcular(request);
            return Ok(response);
        }

        private static bool ValidarModelo(CdbRequest request, out string mensagemErro)
        {
            var context = new ValidationContext(request);
            var resultados = new List<ValidationResult>();
            var valido = Validator.TryValidateObject(request, context, resultados, true);
            mensagemErro = valido ? null : string.Join(" ", resultados.Select(r => r.ErrorMessage));
            return valido;
        }
    }
}
