using Smartwyre.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Data.Repositories.Definitions
{
    public interface IDataRepository
    {
        Product GetProduct(string productIdentifier);
        Rebate GetRebate(string rebateIdentifier);
        void StoreCalculationResult(Rebate account, decimal rebateAmount);
        void PopulateDB();
    }
}
