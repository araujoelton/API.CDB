namespace CDB.Api.Models;

public class ImpostoModel
{
    public decimal Lucro { get; }
    public int PrazoMeses { get; }

    public ImpostoModel(decimal lucro, int prazoMeses)
    {
        Lucro = lucro;
        PrazoMeses = prazoMeses;
    }
}
