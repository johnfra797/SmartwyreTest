using Smartwyre.Data.DTO;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    void PopulateDB();
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
