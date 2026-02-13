using System;
using CDB.Api.Models;
using CDB.Api.Services;
using CDB.Api.Utils;
using Xunit;

namespace CDB.Api.Tests;

public class ImpostoCalculatorTests
{
    [Theory]
    [InlineData(2, 22.5)]
    [InlineData(6, 22.5)]
    [InlineData(7, 20.0)]
    [InlineData(12, 20.0)]
    [InlineData(13, 17.5)]
    [InlineData(24, 17.5)]
    [InlineData(25, 15.0)]
    [InlineData(60, 15.0)]
    public void CalcularImposto_QuandoPrazoPassaPorTodasAsFaixas_EntaoAplicaAliquotaEsperada(int prazoMeses, decimal impostoEsperado)
    {
        // Arrange
        var calculator = CriarCalculator();
        var model = new ImpostoModel(100m, prazoMeses);

        // Act
        var resultado = calculator.CalcularImposto(model);

        // Assert
        Assert.Equal(impostoEsperado, resultado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void CalcularImposto_QuandoPrazoForInvalido_EntaoLancaArgumentOutOfRangeException(int prazoMeses)
    {
        // Arrange
        var calculator = CriarCalculator();
        var model = new ImpostoModel(100m, prazoMeses);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalcularImposto(model));
        Assert.Equal("PrazoMeses", exception.ParamName);
    }

    private static ImpostoCalculator CriarCalculator()
    {
        var tabela = new TabelaAliquotasIrCdb(new ConfigProvider());
        return new ImpostoCalculator(tabela);
    }
}
