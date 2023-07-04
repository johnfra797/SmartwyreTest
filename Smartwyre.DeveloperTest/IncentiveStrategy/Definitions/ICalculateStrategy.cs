using Smartwyre.Data.DTO;
using Smartwyre.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.IncentiveStrategy.Definitions
{
    public interface ICalculateStrategy
    {
        CalculateResult Calculate(CalculateRebateRequest request, Rebate rebate, Product product);
    }
}
