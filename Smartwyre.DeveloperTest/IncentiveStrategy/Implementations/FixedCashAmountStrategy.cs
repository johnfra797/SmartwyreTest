using Smartwyre.Data.DTO;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using Smartwyre.DeveloperTest.IncentiveStrategy.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.IncentiveStrategy.Implementations
{
    public class FixedCashAmountStrategy : ICalculateStrategy
    {
        public CalculateResult Calculate(CalculateRebateRequest request,Rebate rebate, Product product)
        {
            CalculateResult result = new CalculateResult();

            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount = rebate.Amount;
                result.Success = true;
            }
            return result;
        }
    }
}
