using CGTOnboardingTool.Securities;
using CGTOnboardingTool.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool
{
    //Author: Aidan Neil
    public class Report
    {

        private LinkedList<ReportEntry> entries; // A linked list holding all report entries in cronilogical order
        private List<ReportEntry> entriesUnordered; // A list of entries in the order that they are entered

        private int count; // Number of entries in a report
        private List<Security> securities; // All the secuirties that have been referenced within a report

        private Dictionary<Security, List<DateOnly>> securityDates; // The dates where there has been an action on a security
        private Dictionary<Security, List<ReportEntry>> securityEntries; // The cronilogical ordering of entries related to a security

        private Dictionary<Security, List<Security>> relatedSecurities; // A list of securities that are related through some CGTFunciton


        public Report()
        {
            this.entries = new LinkedList<ReportEntry>();
            this.entriesUnordered = new List<ReportEntry>();
            this.count = 0;
            this.securities = new List<Security>();
            this.securityDates = new Dictionary<Security, List<DateOnly>>();
            this.securityEntries = new Dictionary<Security, List<ReportEntry>>();
            this.relatedSecurities = new Dictionary<Security, List<Security>>();
        }

        public ReportEntry[] Rows()
        {
            return entries.ToArray();
        }

        public bool HasSecurity(Security security)
        {
            return securities.Contains(security);
        }

        public Security[] GetSecurities()
        {
            return this.securities.ToArray();
        }

        // AddSecurityAction
        //
        // Given a security, adds it to the overall report alongside the date of the action.
        // Appends to securities and adds to securityDates[s] in sorted order.
        private void AddSecurityActionDate(Security security, DateOnly date)
        {
            if (!this.HasSecurity(security))
            {
                this.securities.Add(security);
                List<DateOnly> dateList = new List<DateOnly> { date };
                this.securityDates.Add(security, dateList);
                List<ReportEntry> entryList = new List<ReportEntry>();
                this.securityEntries.Add(security, entryList);
            }
            else
            {
                var dates = this.securityDates[security];
                var lastDate = dates[-1];
                if (date < lastDate)
                {
                    dates.Add(date);
                    dates.Sort();
                }
                else
                {
                    dates.Add(date);
                }
            }
        }

        public ReportEntry Add(CGTFunction function, DateOnly date, Security security, decimal price, decimal quantity, decimal associatedCosts, decimal gainLoss, decimal section104)
        {
            // 1. Add security action
            this.AddSecurityActionDate(security, date);

            // 2. Calculate what the updated holdings will be
            var holdings = this.GetHoldings(security, date) + quantity;

            // 3. Construct Entry
            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                security: security,
                price: price,
                quantity: quantity,
                associatedCosts: associatedCosts,
                gainLoss: gainLoss,
                holdings: holdings,
                section104: section104
                );

            // 4. Insert entry in correct place
            var securityActionDates = securityDates[security];
            var dateIndex = securityActionDates.IndexOf(date);

            var securityEntryList = securityEntries[security];
            if (dateIndex == 0)
            {
                securityEntryList.Add(newEntry);
                entries.AddLast(newEntry);
                entriesUnordered.Add(newEntry);
            }
            else
            {
                var previous = securityEntryList[dateIndex - 1];
                securityEntryList.Insert(dateIndex, newEntry);
                var previousNode = entries.Find(previous);
                if (previousNode != null)
                {
                    entries.AddAfter(previousNode, newEntry);
                }
                else
                {
                    entries.AddLast(newEntry);
                }
            }

            // 5. Return the entry
            return newEntry;
        }

        public decimal GetSection104(Security security, DateOnly date)
        {
            // 1. Get last action date index
            if (!this.HasSecurity(security))
            {
                return 0;
            }
            var securityActionDates = this.securityDates[security];
            var index = securityActionDates.Count - 1;
            var dateCounter = securityActionDates[index];
            while (securityActionDates[index] >= date && index > -1)
            {
                index = index - 1;
                if (index >= 0)
                {
                    dateCounter = securityActionDates[index];
                }
            }

            // 2. Return the S104 at that date using the fetched index
            if (index == -1)
            {
                return 0;
            }
            else
            {
                var securityEntries = this.securityEntries[security];
                var entry = securityEntries[index];
                return entry.Section104[security];
            }
        }

        public decimal GetHoldings(Security security, DateOnly date)
        {
            // 1. Get last action date index
            if (!this.HasSecurity(security))
            {
                return 0;
            }
            var securityActionDates = this.securityDates[security];
            var index = securityActionDates.Count - 1;
            var dateCounter = securityActionDates[index];
            while (index > -1 && dateCounter >= date)
            {
                index = index - 1;
                if (index >= 0)
                {
                    dateCounter = securityActionDates[index];
                }
            }
            // 2. Return the S104 at that date using the fetched index
            if (index == -1)
            {
                return 0;
            }
            else
            {
                var securityEntries = this.securityEntries[security];
                var entry = securityEntries[index];
                return entry.Holdings[security];
            }
        }


        public ReportEntry Add(CGTFunction function, DateOnly date, Security[] securities, decimal[] prices, decimal[] quantities, decimal associatedCost, decimal[] gainLoss,  decimal[] section104s)
        {
            decimal[] currentHoldings = new decimal[securities.Length];
            decimal[] holdings = new decimal[securities.Length];
           
            for (int i = 0; i < securities.Length; i++)
            {
                this.AddSecurityActionDate(security: securities[i], date: date);
                currentHoldings[i] = this.GetHoldings(security: securities[i], date: date);
                holdings[i] = currentHoldings[i] + quantities[i];
            }
            
            var associatedCostArr = new decimal[] { associatedCost };
            var newEntry = new ReportEntry(
                id: count++,
                function: function,
                date: date,
                securities: securities,
                prices: prices,
                quantities: quantities,
                associatedCosts: new decimal[] {associatedCost},
                gainLoss: gainLoss,
                holdings: holdings,
                section104s: section104s
                );







            throw new NotImplementedException();
        }
    }

}