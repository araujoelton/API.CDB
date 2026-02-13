using CDB.Api.Models;

namespace CDB.Api.Services;

public interface ICdbCalculator
{
    CdbResponse Calcular(CdbRequest request);
}
