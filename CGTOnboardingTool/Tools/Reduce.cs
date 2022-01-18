using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Reduce : CGTFunction
    {
        Security security;
        decimal quantity;
        decimal? pps;
        decimal? cost;
        decimal? gross;
        DateOnly date;

        public Reduce(Security security, decimal quantity, decimal pps, decimal cost, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.pps = pps;
            this.cost = cost;
            this.date = date;
        }

        public Reduce(Security security, decimal quantity, decimal gross, DateOnly date)
        {
            this.security = security;
            this.quantity = quantity;
            this.gross = gross;
            this.date = date;
        }


        public override ReportEntry perform(ref Report report)
        {
            if (gross != null)
            {
                return this.performUsingGross(ref report);
            }
            else if (pps != null && cost != null)
            {
                return this.performUsingQuantityPriceCost(ref report);
            }
            else
            {
                throw new Exception("Build not allowed. Reconstruct build and specify either a Price & Cost pair, or a Gross");
            }
        }

        private ReportEntry performUsingGross(ref Report report)
        {
            // implement reduce using gross

            throw new NotImplementedException();
        }

        private ReportEntry performUsingQuantityPriceCost(ref Report report)
        {
            throw new NotImplementedException();


            //decimal pps = (decimal)this.pps;
            //decimal cost = (decimal)this.cost;

            //decimal currentHoldings = 0;
            //decimal currentS104 = 0;
            //if (!report.HasSecurity(security))
            //{
            //    throw new ArgumentException("Security not in holdings. Cannot reduce");
            //}
            //else
            //{
            //    var retrievedHoldings = report.GetHoldings(security);
            //    if (retrievedHoldings != null)
            //    {
            //        currentHoldings = (decimal)retrievedHoldings;
            //    }
            //    var retrievedS104 = report.GetSection104(security);
            //    if (retrievedS104 != null)
            //    {
            //        currentS104 = (decimal)retrievedS104;
            //    }
            //}

            //var newHoldings = currentHoldings - quantity;

            //var reductionRatio = quantity / currentHoldings;
            //var newS104 = (1 - reductionRatio) * currentS104;

            //Security[] securityAffected = new Security[1] { security };
            //Dictionary<Security, decimal> priceAffected = new Dictionary<Security, decimal>();
            //priceAffected.Add(security, pps);
            //Dictionary<Security, decimal> quantityAffected = new Dictionary<Security, decimal>();
            //quantityAffected.Add(security, -(quantity));
            //decimal[] costs = new decimal[1] { (decimal)cost };
            //Dictionary<Security, decimal> s104 = new Dictionary<Security, decimal>();
            //s104.Add(security, newS104);

            //var associatedEntry = report.Add(this, securityAffected, priceAffected, quantityAffected, costs, s104, date);

            //report.UpdateHoldings(associatedEntry, security, newHoldings);
            //report.UpdatePrice(associatedEntry, security, pps);
            //report.UpdateSection104(associatedEntry, security, newS104);


            //return associatedEntry;

        }
    }
}
