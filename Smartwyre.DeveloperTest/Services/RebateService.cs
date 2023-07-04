using Smartwyre.Data.DTO;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using Smartwyre.Data.Repositories.Definitions;
using Smartwyre.DeveloperTest.IncentiveStrategy.Implementations;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private CalculateContext _calculateContext;
    private IDataRepository _dataRepository;

    public RebateService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _dataRepository.GetRebate(request.RebateIdentifier);
        Product product = _dataRepository.GetProduct(request.ProductIdentifier);
        var result = new CalculateRebateResult();
        if (rebate == null)
        {
            result.Success = false;
        }
        else
        {
            _calculateContext = new CalculateContext(rebate.Incentive,_dataRepository);
            result = _calculateContext.Calculate(request,rebate, product);
        }
        return result;
    }

    public void PopulateDB()
    {
        _dataRepository.PopulateDB();
    }
}
