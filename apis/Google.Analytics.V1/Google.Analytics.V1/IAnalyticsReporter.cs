using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google.Analytics.V1
{
    public interface IAnalyticsReporter
    {
        void ReportEvent(string category, string action, string label = null, int? value = null);

        void ReportPageView(string page, string title, Dictionary<int, string> customDimensions = null);
    }
}
