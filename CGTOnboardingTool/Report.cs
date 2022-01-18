using CGTOnboardingTool.Securities;
using CGTOnboardingTool.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Old
{
    public class Report
    {

        public record Section104Log
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal Section104 { get; init; }
        }

        public record SecurityHoldingLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal Quantitiy { get; init; }
        }

        public record SecurityPriceLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal Price { get; init; }
        }

        public record SecurityAllowableCostsLog
        {
            public int AssociatedReportEntryID { get; init; }
            public Security Security { get; init; }
            public decimal AllowableCost { get; init; }
        }

        private List<ReportEntry> _reportList = new();

        private List<Security> _securities = new();

        private Dictionary<Security, decimal> _securityPrices = new();
        private List<SecurityPriceLog> _securityPriceHistory = new();

        private Dictionary<Security, decimal> _holdings = new();
        private List<SecurityHoldingLog> _holdingsHistory = new();

        private Dictionary<Security, decimal> _section104 = new();
        private List<Section104Log> _section104History = new();

        private Dictionary<Security, decimal> _securityAllowableCosts = new();
        private List<SecurityAllowableCostsLog> _securityAllowableCostsHistory = new();

        private Dictionary<Security, Security> _relatedSecurities = new();
        private int _rowCount = 0;

        public List<ReportEntry> Rows()
        {
            return this._reportList;
        }

        public int Count()
        {
            return this._rowCount;
        }

        public List<ReportEntry> Copy()
        {
            List<ReportEntry> reportCpy = new();
            foreach (ReportEntry entry in _reportList)
            {
                reportCpy.Add(entry);
            }
            return reportCpy;
        }

        public bool HasSecurity(Security s)
        {
            return _securities.Contains(s);
        }

        public void AddSecurity(Security sec)
        {
            if (!this.HasSecurity(sec))
            {
                this._securities.Add(sec);
            }
        }

        public List<Security> GetSecurities()
        {
            List<Security> secList = new List<Security>();
            foreach (var sec in this._securities)
            {
                secList.Add(sec);
            }
            return secList;
        }

        public Nullable<decimal> GetLastSecurityPrice(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._securityPrices[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityPriceLog> GetSecurityPriceHistory(Security security)
        {
            List<SecurityPriceLog> list = new();

            foreach (SecurityPriceLog entry in _securityPriceHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public Nullable<decimal> GetHoldings(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._holdings[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityHoldingLog> GetHoldingsHistory(Security security)
        {
            List<SecurityHoldingLog> list = new();

            foreach (SecurityHoldingLog entry in _holdingsHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }

        public decimal GetHoldingsOrDefault(Security security, decimal suppliedDefault)
        {
            var holdings = this.GetHoldings(security);
            if (holdings == null)
            {
                return suppliedDefault;
            }
            return (decimal)holdings;
        }

        public Nullable<decimal> GetAllowableCost(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._securityAllowableCosts[security]);
            }
            else
            {
                return null;
            }
        }

        public List<SecurityAllowableCostsLog> GetAllowableCostsHistory(Security security)
        {
            List<SecurityAllowableCostsLog> list = new();

            foreach (SecurityAllowableCostsLog entry in _securityAllowableCostsHistory)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public Nullable<decimal> GetSection104(Security security)
        {
            if (this.HasSecurity(security))
            {
                return (this._section104[security]);
            }
            else
            {
                return null;
            }
        }

        public decimal GetSection104OrDefault(Security security, decimal suppliedDefault)
        {
            var s104 = this.GetSection104(security);
            if (s104 == null)
            {
                return suppliedDefault;
            }
            return (decimal)s104;
        }

        public List<Section104Log> GetSection104History(Security security)
        {
            List<Section104Log> list = new();

            foreach (Section104Log entry in _section104History)
            {
                if (entry.Security.Equals(security))
                {
                    list.Add(entry);
                }
            }
            return list;
        }


        public void UpdateSection104(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._section104[security] = newValue;
            var log = new Section104Log
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Section104 = newValue
            };
            this._section104History.Add(log);
        }

        public void UpdateHoldings(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            if (this._holdings.ContainsKey(security))
            {
                this._holdings[security] = newValue;
            }
            else
            {
                this._holdings.Add(security, newValue);
            }
            var log = new SecurityHoldingLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Quantitiy = newValue
            };
            this._holdingsHistory.Add(log);
        }

        public void UpdatePrice(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._securityPrices[security] = newValue;
            var log = new SecurityPriceLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                Price = newValue,
            };
        }

        public void UpdateAllowableCost(ReportEntry associatedEntry, Security security, decimal newValue)
        {
            this._securityAllowableCosts[security] = newValue;
            var log = new SecurityAllowableCostsLog
            {
                AssociatedReportEntryID = associatedEntry.EntryID,
                Security = security,
                AllowableCost = newValue,
            };
        }

        public ReportEntry Add(CGTFunction FunctionPerformed, Security[] SecuritiesAffected, Dictionary<Security, decimal> PricesAffected, Dictionary<Security, decimal> QuantitiesAffected, decimal[] AssociatedCosts, Dictionary<Security, decimal> Section104sAfter, DateOnly DatePerformed)
        {
            var entry = new ReportEntry
            {
                EntryID = _rowCount + 1,
                DatePerformed = DatePerformed,
                FunctionPerformed = FunctionPerformed,
                SecuritiesAffected = SecuritiesAffected,
                PricesAffected = PricesAffected,
                QuantitiesAffected = QuantitiesAffected,
                AssociatedCosts = AssociatedCosts,
                Section104sAfter = Section104sAfter
            };
            _reportList.Add(entry);
            _rowCount++;
            return entry;
        }


    }

    public record ReportEntry
    {
        public int EntryID { get; init; }
        public DateOnly DatePerformed { get; init; }
        public CGTFunction FunctionPerformed { get; init; }
        public Security[] SecuritiesAffected { get; init; }
        public Dictionary<Security, decimal> PricesAffected { get; init; }
        public Dictionary<Security, decimal> QuantitiesAffected { get; init; }
        public decimal[] AssociatedCosts { get; init; }
        public Dictionary<Security, decimal> Section104sAfter { get; init; }

        public override string ToString()
        {
            var sec = new StringBuilder();
            foreach (var e in SecuritiesAffected)
            {
                sec.Append(e.ShortName + ",");
            }
            sec.Remove(sec.Length - 1, 1);

            var p = new StringBuilder();
            p.Append("{");
            foreach (var e in PricesAffected)
            {
                p.Append(String.Format("{0} : {1},", e.Key.ShortName, e.Value));
            }
            p.Remove(p.Length - 1, 1);
            p.Append("}");

            var q = new StringBuilder();
            q.Append("{");
            foreach (var e in QuantitiesAffected)
            {
                q.Append(String.Format("{0} : {1},", e.Key.ShortName, e.Value));
            }
            q.Remove(q.Length - 1, 1);
            q.Append("}");

            var c = new StringBuilder();
            foreach (var e in AssociatedCosts)
            {
                c.Append(e + ",");
            }
            c.Remove(c.Length - 1, 1);

            var s104 = new StringBuilder();
            s104.Append("{");
            foreach (var e in Section104sAfter)
            {
                s104.Append(String.Format("{0} : {1},", e.Key.ShortName, e.Value.ToString("F")));
            }
            s104.Remove(s104.Length - 1, 1);
            s104.Append("}");

            return String.Format("Entry ID: {0}, Date: {1}, CGTFunction: {2}, Securities: {3}, Prices: {4}, Quantities: {5}, AssociatedCosts: {6}, Section104s: {7}",
                this.EntryID,
                this.DatePerformed,
                this.FunctionPerformed.GetType().Name.ToString(),
                sec,
                p,
                q,
                c,
                s104);
        }
    }

    
}
