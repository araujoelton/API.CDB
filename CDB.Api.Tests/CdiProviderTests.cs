using System;
using CDB.Api.Services;
using CDB.Api.Utils;
using Xunit;

namespace CDB.Api.Tests;

public class CdiProviderTests
{
    [Fact]
    public void ObterTaxa_QuandoVariavelDeAmbienteNaoExiste_EntaoRetornaValorPadrao()
    {
        // Arrange
        var provider = new CdiProvider(new ConfigProvider());

        // Act
        var resultado = provider.ObterTaxa();

        // Assert
        Assert.Equal(0.009m, resultado);
    }

    [Fact]
    public void ObterTaxa_QuandoVariavelDeAmbienteExiste_EntaoRetornaValorDaVariavel()
    {
        // Arrange
        var valorOriginal = Environment.GetEnvironmentVariable("CDI");

        try
        {
            Environment.SetEnvironmentVariable("CDI", "0.012");
            var provider = new CdiProvider(new ConfigProvider());

            // Act
            var resultado = provider.ObterTaxa();

            // Assert
            Assert.Equal(0.012m, resultado);
        }
        finally
        {
            Environment.SetEnvironmentVariable("CDI", valorOriginal);
        }
    }
}
