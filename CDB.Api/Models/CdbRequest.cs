using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CDB.Api.Models;

public class CdbRequest
{
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
    public decimal Valor { get; }

    [Required(ErrorMessage = "O prazo é obrigatório.")]
    [Range(2, int.MaxValue, ErrorMessage = "O prazo deve ser maior que 1 mês.")]
    public int PrazoMeses { get; }

    public CdbRequest()
    {
    }

    [JsonConstructor]
    public CdbRequest(decimal valor, int prazoMeses)
    {
        Valor = valor;
        PrazoMeses = prazoMeses;
    }
}
