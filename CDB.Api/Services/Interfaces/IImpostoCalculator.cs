using CDB.Api.Models;

namespace CDB.Api.Services;

public interface IImpostoCalculator
{
    decimal CalcularImposto(ImpostoModel model);
}
