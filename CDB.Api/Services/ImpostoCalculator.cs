using System;
using CDB.Api.Models;

namespace CDB.Api.Services;

public class ImpostoCalculator : IImpostoCalculator
{
    private readonly ITabelaAliquotas _tabela;

    public ImpostoCalculator(ITabelaAliquotas tabela)
    {
        _tabela = tabela ?? throw new ArgumentNullException(nameof(tabela));
    }

    public decimal CalcularImposto(ImpostoModel model)
    {
        if (model.PrazoMeses < 2)
            throw new ArgumentOutOfRangeException(nameof(model.PrazoMeses), model.PrazoMeses, "O prazo deve ser maior que 1.");

        decimal aliquota = _tabela.ObterAliquota(model.PrazoMeses);
        return model.Lucro * aliquota;
    }
}
