using System;
using CDB.Api.Services;
using CDB.Api.Utils;
using Xunit;

namespace CDB.Api.Tests;

public class TbProviderTests
{
    [Fact]
    public void ObterTaxa_QuandoVariavelDeAmbienteNaoExiste_EntaoRetornaValorPadrao()
    {
        // Arrange
        var provider = new TbProvider(new ConfigProvider());

        // Act
        var resultado = provider.ObterTaxa();

        // Assert
        Assert.Equal(1.08m, resultado);
    }

    [Fact]
    public void ObterTaxa_QuandoVariavelDeAmbienteExiste_EntaoRetornaValorDaVariavel()
    {
        // Arrange
        var valorOriginal = Environment.GetEnvironmentVariable("TB");

        try
        {
            Environment.SetEnvironmentVariable("TB", "1.10");
            var provider = new TbProvider(new ConfigProvider());

            // Act
            var resultado = provider.ObterTaxa();

            // Assert
            Assert.Equal(1.10m, resultado);
        }
        finally
        {
            Environment.SetEnvironmentVariable("TB", valorOriginal);
        }
    }
}
