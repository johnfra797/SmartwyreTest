using Azure.Core;
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
    public class FixedRateRebateStrategy : ICalculateStrategy
    {
        public CalculateResult Calculate(CalculateRebateRequest request,Rebate rebate, Product product)
        {
            CalculateResult result = new CalculateResult();
            if (product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                result.Success = false;
            }
            else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount += product.Price * rebate.Percentage * request.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
