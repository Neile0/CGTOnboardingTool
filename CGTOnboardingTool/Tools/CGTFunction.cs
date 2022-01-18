using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTOnboardingTool.Tools
{
    public abstract class CGTFunction
    {
        public abstract ReportEntry perform(ref Report report);
    }
}
