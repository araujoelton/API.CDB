namespace CDB.Api.Models;

public class CdbResponse
{
    public decimal ValorBruto { get; }
    public decimal ValorLiquido { get; }
    public decimal Imposto { get; }

    public CdbResponse(decimal valorBruto, decimal valorLiquido, decimal imposto)
    {
        ValorBruto = valorBruto;
        ValorLiquido = valorLiquido;
        Imposto = imposto;
    }
}
