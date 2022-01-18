using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Securities
{
    public class Security
    {
        public String Name { get; init; }
        public String ShortName { get; init; }
        
        
        public Security(string Name, String ShortName)
        {
            this.Name = Name;
            this.ShortName = ShortName;
        }

        public override string ToString()
        {
            return String.Format("\"{0}\" - {1}", this.ShortName, this.Name);
        }

        public override bool Equals(object? obj)
        {
            return obj is Security security &&
                   Name == security.Name &&
                   ShortName == security.ShortName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ShortName);
        }
    }
}