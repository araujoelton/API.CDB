using System;
using System.Configuration;
using System.Globalization;

namespace CDB.Api.Utils;

public class ConfigProvider : IConfigProvider
{
    public int ObterConfig(string chave, int padrao)
    {
        var valor = ConfigurationManager.AppSettings[chave];
        return int.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result : padrao;
    }

    public decimal ObterConfig(string chave, decimal padrao)
    {
        var valor = ConfigurationManager.AppSettings[chave];
        return decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result : padrao;
    }

    public string ObterConfig(string chave, string padrao)
    {
        var valor = ConfigurationManager.AppSettings[chave];
        return string.IsNullOrEmpty(valor) ? padrao : valor;
    }

    public decimal ObterConfigComEnv(string chaveEnv, string chaveConfig, decimal padrao)
    {
        var valor = Environment.GetEnvironmentVariable(chaveEnv) ?? ConfigurationManager.AppSettings[chaveConfig];
        return decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result : padrao;
    }
}
