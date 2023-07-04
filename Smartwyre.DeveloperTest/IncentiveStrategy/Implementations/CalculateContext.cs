using Smartwyre.Data.DTO;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using Smartwyre.Data.Repositories.Definitions;
using Smartwyre.Data.Repositories.Implementations;
using Smartwyre.DeveloperTest.IncentiveStrategy.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.IncentiveStrategy.Implementations
{
    public class CalculateContext
    {
        private ICalculateStrategy _strategy;
        private IDataRepository _dataRepository;

        Dictionary<IncentiveType, ICalculateStrategy> strategies = new Dictionary<IncentiveType, ICalculateStrategy>() {
            { IncentiveType.FixedCashAmount, new FixedCashAmountStrategy() },
            { IncentiveType.FixedRateRebate, new FixedRateRebateStrategy() },
            { IncentiveType.AmountPerUom, new AmountPerUomStrategy() }
        };

        public CalculateContext(IncentiveType incentiveType, IDataRepository dataRepository)
        {
            _strategy = strategies[incentiveType];
            _dataRepository = dataRepository;
        }

        public CalculateRebateResult Calculate(CalculateRebateRequest request,Rebate rebate, Product product)
        {
            var calculateResult = _strategy.Calculate(request, rebate, product);

            if (calculateResult.Success)
            {
                _dataRepository.StoreCalculationResult(rebate, calculateResult.RebateAmount);
            }
            CalculateRebateResult result = new CalculateRebateResult();
            result.Success = calculateResult.Success;
            return result;
        }
    }
}