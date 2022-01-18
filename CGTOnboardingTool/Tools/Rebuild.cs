using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public class Rebuild : CGTFunction
    {

        Security oldSecurity;
        decimal oldQuantityReduce;
        Security newSecurity;
        decimal newQuantityBuild;
        DateOnly date;

        public Rebuild(Security oldSecurity, decimal quantityToReduce, Security newSecurity, decimal quantityToBuild, DateOnly date)
        {
            this.oldSecurity = oldSecurity;
            this.oldQuantityReduce = quantityToReduce;
            this.newSecurity = newSecurity;
            this.newQuantityBuild = quantityToBuild;
            this.date = date;
        }

        public override ReportEntry perform(ref Report report)
        {

            throw new NotImplementedException();

            //decimal currentHoldingsOldSecurity = 0;
            //decimal currentHoldingNewSecurity = 0;
            //decimal currentS104OldSecuirty = 0;
            //decimal currentS104NewSecurity = 0;
            //if (!report.HasSecurity(newSecurity)){
            //    report.AddSecurity(newSecurity);
            //}
            //else
            //{
            //    var retrievedHoldingsNewSecurity = report.GetHoldings(newSecurity);
            //    if (retrievedHoldingsNewSecurity != null)
            //    {
            //        currentHoldingNewSecurity = (decimal)retrievedHoldingsNewSecurity;
            //    }

            //    var retrievedS104NewSecurity = report.GetSection104(newSecurity);
            //    if (retrievedS104NewSecurity != null)
            //    {
            //        currentS104NewSecurity = (decimal)retrievedS104NewSecurity;
            //    }
            //}

            //var retrievedHoldingsOldSecurity = report.GetHoldings(oldSecurity);
            //if (retrievedHoldingsOldSecurity != null)
            //{
            //    currentHoldingsOldSecurity = (decimal)retrievedHoldingsOldSecurity;
            //}

            //var retrievedS104OldSecurity = report.GetSection104(oldSecurity);
            //if (retrievedS104OldSecurity != null)
            //{
            //    currentS104OldSecuirty = (decimal)retrievedS104OldSecurity;
            //}

            //var reductionRatio = oldQuantityReduce / currentHoldingsOldSecurity;

            //var newS104NewSecurity = currentS104NewSecurity + (reductionRatio * currentS104OldSecuirty);
            //var newS104OldSecurity = (1 - reductionRatio) * currentS104OldSecuirty;

            //var newHoldingsOldSecurity = currentHoldingsOldSecurity - oldQuantityReduce;
            //var newHoldingsNewSecurity = currentHoldingNewSecurity + newQuantityBuild;

            //Security[] securitiesAffected = new Security[2] { oldSecurity, newSecurity };
            //Dictionary<Security, decimal> quantitiesAffected = new Dictionary<Security, decimal>();
            //quantitiesAffected.Add(oldSecurity, -(oldQuantityReduce));
            //quantitiesAffected.Add(newSecurity, newQuantityBuild);

            //Dictionary<Security, decimal> s104s = new Dictionary<Security, decimal>();
            //s104s.Add(oldSecurity, newS104OldSecurity);
            //s104s.Add(newSecurity, newS104NewSecurity);

            //var associatedEntry = report.Add(FunctionPerformed: this, SecuritiesAffected: securitiesAffected, PricesAffected: new Dictionary<Security, decimal>(), QuantitiesAffected: quantitiesAffected, AssociatedCosts: new decimal[1],Section104sAfter: s104s, DatePerformed:date);

            //report.UpdateHoldings(associatedEntry, oldSecurity, newHoldingsOldSecurity);
            //report.UpdateHoldings(associatedEntry,newSecurity, newHoldingsNewSecurity);

            //report.UpdateSection104(associatedEntry, oldSecurity, newS104OldSecurity);
            //report.UpdateSection104(associatedEntry, newSecurity, newS104NewSecurity);

            //return associatedEntry;
        }
    }
}
