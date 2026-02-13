using CDB.Api.Utils;
using Xunit;

namespace CDB.Api.Tests;

public class ConfigProviderTests
{
    [Fact]
    public void ObterConfig_Int_QuandoChaveNaoExiste_RetornaPadrao()
    {
        var provider = new ConfigProvider();

        var resultado = provider.ObterConfig("CHAVE_INT_NAO_EXISTENTE", 123);

        Assert.Equal(123, resultado);
    }

    [Fact]
    public void ObterConfig_String_QuandoChaveNaoExiste_RetornaPadrao()
    {
        var provider = new ConfigProvider();

        var resultado = provider.ObterConfig("CHAVE_STRING_NAO_EXISTENTE", "valor-padrao");

        Assert.Equal("valor-padrao", resultado);
    }
}
