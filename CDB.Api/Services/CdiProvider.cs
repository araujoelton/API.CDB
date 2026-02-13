using System;
using CDB.Api.Utils;

namespace CDB.Api.Services;

public class CdiProvider : ICdiProvider
{
    private const decimal Padrao = 0.009m;
    private readonly IConfigProvider _config;

    public CdiProvider(IConfigProvider config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public decimal ObterTaxa()
    {
        return _config.ObterConfigComEnv("CDI", "Cdi", Padrao);
    }
}
