using System;
using System.Globalization;
using CDB.Api.Models;
using CDB.Api.Services;
using CDB.Api.Utils;
using Moq;
using Xunit;

namespace CDB.Api.Tests;

public class CalculatorTests
{
    private const decimal TaxaCdiPadrao = 0.009m;
    private const decimal TaxaTbPadrao = 1.08m;

    [Fact]
    public void Calcular_QuandoRequestForNulo_EntaoLancaArgumentNullException()
    {
        // Arrange
        var calculator = CriarCalculatorComImpostoMockado();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => calculator.Calcular(null));
    }

    [Theory]
    [InlineData(2, "1019.53", "4.39", "1015.14")]
    [InlineData(6, "1059.75", "13.44", "1046.31")]
    [InlineData(7, "1070.05", "14.01", "1056.04")]
    [InlineData(12, "1123.07", "24.61", "1098.46")]
    [InlineData(13, "1133.99", "23.45", "1110.54")]
    [InlineData(24, "1261.31", "45.73", "1215.58")]
    [InlineData(25, "1273.57", "41.04", "1232.53")]
    [InlineData(36, "1416.57", "62.49", "1354.08")]
    public void Calcular_QuandoPrazoPassaPorTodasAsFaixas_EntaoRetornaBrutoImpostoELiquidoEsperados(
        int prazoMeses,
        string valorBrutoEsperadoTexto,
        string impostoEsperadoTexto,
        string valorLiquidoEsperadoTexto)
    {
        // Arrange
        var calculator = CriarCalculatorComRegrasReais();
        var request = new CdbRequest(1000m, prazoMeses);
        var valorBrutoEsperado = decimal.Parse(valorBrutoEsperadoTexto, CultureInfo.InvariantCulture);
        var impostoEsperado = decimal.Parse(impostoEsperadoTexto, CultureInfo.InvariantCulture);
        var valorLiquidoEsperado = decimal.Parse(valorLiquidoEsperadoTexto, CultureInfo.InvariantCulture);

        // Act
        var resultado = calculator.Calcular(request);

        // Assert
        Assert.Equal(valorBrutoEsperado, resultado.ValorBruto);
        Assert.Equal(impostoEsperado, resultado.Imposto);
        Assert.Equal(valorLiquidoEsperado, resultado.ValorLiquido);
    }

    [Fact]
    public void Calcular_QuandoExecutado_EntaoEnviaLucroEPrazoCorretosParaCalculadoraDeImposto()
    {
        // Arrange
        ImpostoModel modelCapturado = null;
        var cdiMock = CriarCdiProviderMockComTaxaPadrao();
        var tbMock = CriarTbProviderMockComTaxaPadrao();
        var impostoMock = new Mock<IImpostoCalculator>();
        impostoMock
            .Setup(x => x.CalcularImposto(It.IsAny<ImpostoModel>()))
            .Callback<ImpostoModel>(model => modelCapturado = model)
            .Returns(10m);

        var calculator = new Calculator(cdiMock.Object, tbMock.Object, impostoMock.Object);
        var request = new CdbRequest(1000m, 12);

        // Act
        calculator.Calcular(request);

        // Assert
        Assert.NotNull(modelCapturado);
        Assert.True(modelCapturado.Lucro > 0m);
        Assert.Equal(12, modelCapturado.PrazoMeses);
    }

    [Fact]
    public void Calcular_QuandoExecutado_EntaoRetornaValoresComDuasCasasDecimais()
    {
        // Arrange
        var cdiMock = CriarCdiProviderMockComTaxaPadrao();
        var tbMock = CriarTbProviderMockComTaxaPadrao();
        var impostoMock = new Mock<IImpostoCalculator>();
        impostoMock.Setup(x => x.CalcularImposto(It.IsAny<ImpostoModel>())).Returns(1.111m);

        var calculator = new Calculator(cdiMock.Object, tbMock.Object, impostoMock.Object);
        var request = new CdbRequest(1000m, 3);

        // Act
        var resultado = calculator.Calcular(request);

        // Assert
        Assert.Equal(Math.Round(resultado.ValorBruto, 2), resultado.ValorBruto);
        Assert.Equal(Math.Round(resultado.ValorLiquido, 2), resultado.ValorLiquido);
        Assert.Equal(Math.Round(resultado.Imposto, 2), resultado.Imposto);
    }

    private static Calculator CriarCalculatorComRegrasReais()
    {
        var configProvider = new ConfigProvider();
        var tabelaAliquotas = new TabelaAliquotasIrCdb(configProvider);
        var impostoCalculator = new ImpostoCalculator(tabelaAliquotas);
        var cdiMock = CriarCdiProviderMockComTaxaPadrao();
        var tbMock = CriarTbProviderMockComTaxaPadrao();

        return new Calculator(cdiMock.Object, tbMock.Object, impostoCalculator);
    }

    private static Calculator CriarCalculatorComImpostoMockado()
    {
        var cdiMock = CriarCdiProviderMockComTaxaPadrao();
        var tbMock = CriarTbProviderMockComTaxaPadrao();
        var impostoMock = new Mock<IImpostoCalculator>();
        impostoMock.Setup(x => x.CalcularImposto(It.IsAny<ImpostoModel>())).Returns((ImpostoModel model) => model.Lucro * 0.225m);

        return new Calculator(cdiMock.Object, tbMock.Object, impostoMock.Object);
    }

    private static Mock<ICdiProvider> CriarCdiProviderMockComTaxaPadrao()
    {
        var cdiMock = new Mock<ICdiProvider>();
        cdiMock.Setup(x => x.ObterTaxa()).Returns(TaxaCdiPadrao);
        return cdiMock;
    }

    private static Mock<ITbProvider> CriarTbProviderMockComTaxaPadrao()
    {
        var tbMock = new Mock<ITbProvider>();
        tbMock.Setup(x => x.ObterTaxa()).Returns(TaxaTbPadrao);
        return tbMock;
    }
}
