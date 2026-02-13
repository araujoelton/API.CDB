using System;
using CDB.Api.Utils;

namespace CDB.Api.Services;

public class TbProvider : ITbProvider
{
    private const decimal Padrao = 1.08m;
    private readonly IConfigProvider _config;

    public TbProvider(IConfigProvider config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public decimal ObterTaxa()
    {
        return _config.ObterConfigComEnv("TB", "Tb", Padrao);
    }
}
