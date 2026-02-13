using CDB.Api.Utils;
using System;
using System.Reflection;

namespace CDB.Api.Services;

public class TabelaAliquotasIrCdb : ITabelaAliquotas
{
    private const decimal AliquotaAte6Meses = 0.225m;
    private const decimal AliquotaAte12Meses = 0.20m;
    private const decimal AliquotaAte24Meses = 0.175m;
    private const decimal AliquotaAcima24Meses = 0.15m;

    private readonly IConfigProvider _config;

    public TabelaAliquotasIrCdb(IConfigProvider config)
    {
        _config = config ?? throw new System.ArgumentNullException(nameof(config));
    }

    public decimal ObterAliquota(int prazoMeses)
    {
        if (prazoMeses < 2) throw new ArgumentOutOfRangeException(nameof(prazoMeses), prazoMeses, "O prazo deve ser maior que 1.");

        return prazoMeses switch
        {
            >= 1 and <= 6 => _config.ObterConfig("Imposto_6", AliquotaAte6Meses),
            >= 7 and <= 12 => _config.ObterConfig("Imposto_12", AliquotaAte12Meses),
            >= 13 and <= 24 => _config.ObterConfig("Imposto_24", AliquotaAte24Meses),
            _ => _config.ObterConfig("Imposto_999", AliquotaAcima24Meses)
        };
    }
}
