using CDB.Api.Models;
using Xunit;

namespace CDB.Api.Tests;

public class CdbRequestTests
{
    [Fact]
    public void ConstrutorPadrao_QuandoInstanciado_MantemValoresDefault()
    {
        var request = new CdbRequest();

        Assert.Equal(0m, request.Valor);
        Assert.Equal(0, request.PrazoMeses);
    }
}
