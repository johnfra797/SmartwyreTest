using Smartwyre.Data.Context;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using Smartwyre.Data.Repositories.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Data.Repositories.Implementations
{
    public class DataRepository : IDataRepository
    {
        private SmartwyreContext _context;
        public DataRepository(SmartwyreContext context)
        {
            _context = context;
        }
        public Product GetProduct(string productIdentifier)
        {
            // Access database to retrieve account, code removed for brevity 
            return _context.Products.First(x => x.Identifier == productIdentifier);
        }

        public Rebate GetRebate(string rebateIdentifier)
        {
            // Access database to retrieve account, code removed for brevity 
            return _context.Rebates.First(x => x.Identifier == rebateIdentifier);
        }

        public void PopulateDB()
        {
            Product product = new Product();
            product.Identifier = "b948bc91-0dac-487a-8bb5-0a68eeabf127";
            product.Price = 100;
            product.Uom = string.Empty;
            product.SupportedIncentives = SupportedIncentiveType.FixedCashAmount;
            _context.Products.Add(product);

            Product product2 = new Product();
            product2.Identifier = "3d68e866-c9b1-4f54-ac2e-8d56491e138a";
            product2.Price = 100;
            product2.Uom = string.Empty;
            product2.SupportedIncentives = SupportedIncentiveType.FixedRateRebate;
            _context.Products.Add(product2);

            Product product3 = new Product();
            product3.Identifier = "262eafc4-c688-4108-9d4a-91fae75b36e3";
            product3.Price = 100;
            product3.Uom = string.Empty;
            product3.SupportedIncentives = SupportedIncentiveType.AmountPerUom;
            _context.Products.Add(product3);

            Rebate rebate = new Rebate();
            rebate.Identifier = "803dca12-59c6-4636-bca0-93dc30c7752a";
            rebate.Incentive = IncentiveType.FixedCashAmount;
            rebate.Amount = 100;
            rebate.Percentage = 10;
            _context.Rebates.Add(rebate);

            Rebate rebate2 = new Rebate();
            rebate2.Identifier = "b768868a-5f0a-4fe3-ba82-e8411b8d7cd4";
            rebate2.Incentive = IncentiveType.FixedRateRebate;
            rebate2.Amount = 100;
            rebate2.Percentage = 10;
            _context.Rebates.Add(rebate2);

            Rebate rebate3 = new Rebate();
            rebate3.Identifier = "c1db7af1-b1d9-4402-a5af-dac9be1d4140";
            rebate3.Incentive = IncentiveType.AmountPerUom;
            rebate3.Amount = 100;
            rebate3.Percentage = 10;
            _context.Rebates.Add(rebate3);

            _context.SaveChanges();
        }
        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
            // Update account in database, code removed for brevity
            RebateCalculation rebateCalculation = new RebateCalculation();
            rebateCalculation.RebateIdentifier = account.Identifier;
            rebateCalculation.IncentiveType = account.Incentive;
            rebateCalculation.Amount = rebateAmount;
            rebateCalculation.Identifier = Guid.NewGuid().ToString();
            _context.RebateCalculations.Add(rebateCalculation);
            _context.SaveChanges();
        }
    }
}
