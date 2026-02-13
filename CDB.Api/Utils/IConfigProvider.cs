namespace CDB.Api.Utils;

public interface IConfigProvider
{
    int ObterConfig(string chave, int padrao);
    decimal ObterConfig(string chave, decimal padrao);
    string ObterConfig(string chave, string padrao);
    decimal ObterConfigComEnv(string chaveEnv, string chaveConfig, decimal padrao);
}
