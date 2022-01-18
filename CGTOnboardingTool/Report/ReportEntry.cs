using CGTOnboardingTool.Tools;
using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool
{
    // Author Aidan Neil
    public class ReportEntry
    {
        public int Id { get; init; }
        public CGTFunction Function { get; init; }
        public DateOnly Date { get; init; }
        public Security[] Security { get; init }
        public Dictionary<Security, decimal>? Price { get; init; }
        public Dictionary<Security, decimal>? Quantity { get; init; }
        public decimal[]? AssociatedCosts { get; init; }
        public decimal? Gross { get; init; }
        public decimal[] GainLoss { get; init; }
        public Dictionary<Security, decimal> Holdings { get; set; }
        public Dictionary<Security, decimal> Section104 { get; set; }

        public ReportEntry(int id, CGTFunction function, DateOnly date, Security security, decimal price, decimal quantity, decimal associatedCosts, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[] {security};
            this.Price = new Dictionary<Security, decimal>
            {
                {security, price},
            };
            this.Quantity = new Dictionary<Security, decimal>
            {
                {security, quantity},
            };
            this.AssociatedCosts = new decimal[] { associatedCosts };
            this.Gross = null;
            this.GainLoss = new decimal[] { gainLoss };
            this.Holdings = new Dictionary<Security, decimal>
            {
                {security, holdings}
            };
            this.Section104 = new Dictionary<Security, decimal>
            {
                {security, section104 }
            };
        }

        public ReportEntry(int id, CGTFunction function, DateOnly date, Security security, decimal quantity, decimal gross, decimal gainLoss, decimal holdings, decimal section104)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[] {security};
            this.Price = null;
            this.Quantity = new Dictionary<Security, decimal>
            {
                {security, quantity},
            };
            this.AssociatedCosts = null;
            this.Gross = gross;
            this.GainLoss = new decimal[] { gainLoss };
            this.Holdings = new Dictionary<Security, decimal>
            {
                {security, holdings}
            };
            this.Section104 = new Dictionary<Security, decimal>
            {
                {security, section104 }
            };
        }

        public ReportEntry(int id, CGTFunction function, DateOnly date, Security[] securities, decimal[] prices, decimal[] quantities, decimal[] associatedCosts,decimal[] gainLoss, decimal[] holdings, decimal[] section104s)
        {
            this.Id = id;
            this.Function = function;
            this.Date = date;
            this.Security = new Security[securities.Length];
            for(int i = 0; i < securities.Length; i++)
            {
                this.Security[i] = securities[i];
            }
            this.Price = new Dictionary<Security, decimal>();
            this.Quantity = new Dictionary<Security, decimal>();
            this.AssociatedCosts = new decimal[associatedCosts.Length];
            for (int i = 0; i < associatedCosts.Length; i++)
            {
                this.AssociatedCosts[i] = associatedCosts[i];
            }
            this.Gross = null;
            this.GainLoss = new decimal[gainLoss.Length];
            this.Holdings = new Dictionary<Security, decimal>();
            this.Section104 = new Dictionary<Security, decimal>();
            for (int i = 0; i < securities.Length; i++)
            {

                this.Price.Add(securities[i], prices[i]);
                this.Quantity.Add(securities[i], quantities[i]);
                this.GainLoss[i] = gainLoss[i];
                this.Holdings.Add(securities[i], holdings[i]);
                this.Section104.Add(securities[i], section104s[i]);
            }
        }

    }
}
