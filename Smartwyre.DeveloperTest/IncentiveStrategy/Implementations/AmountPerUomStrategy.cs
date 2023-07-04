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
    public class AmountPerUomStrategy : ICalculateStrategy
    {
        public CalculateResult Calculate(CalculateRebateRequest request,Rebate rebate, Product product)
        {
            CalculateResult result = new CalculateResult();

            if (product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount += rebate.Amount * request.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
