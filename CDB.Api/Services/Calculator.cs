using System;
using CDB.Api.Models;

namespace CDB.Api.Services;

public class Calculator : ICdbCalculator
{
    private readonly ICdiProvider _cdiProvider;
    private readonly ITbProvider _tbProvider;
    private readonly IImpostoCalculator _impostoCalculator;

    public Calculator(ICdiProvider cdiProvider, ITbProvider tbProvider, IImpostoCalculator impostoCalculator)
    {
        _cdiProvider = cdiProvider;
        _tbProvider = tbProvider;
        _impostoCalculator = impostoCalculator;
    }

    public CdbResponse Calcular(CdbRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        decimal cdi = _cdiProvider.ObterTaxa();
        decimal tb = _tbProvider.ObterTaxa();

        decimal valorAtual = request.Valor;
        for (int i = 0; i < request.PrazoMeses; i++)
        {
            valorAtual = valorAtual * (1 + (cdi * tb));
            valorAtual = Math.Round(valorAtual, 2, MidpointRounding.AwayFromZero);
        }

        decimal valorBruto = valorAtual;
        decimal lucro = valorBruto - request.Valor;        
        decimal imposto = Math.Round(_impostoCalculator.CalcularImposto(new ImpostoModel(lucro, request.PrazoMeses)),2,MidpointRounding.AwayFromZero);
        decimal valorLiquido = Math.Round(valorBruto - imposto, 2, MidpointRounding.AwayFromZero);

        return new CdbResponse(valorBruto, valorLiquido, imposto);
    }
}
