using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google.Analytics.V1
{
    /// <summary>
    /// Reports events using the provided analytics reporter.
    /// </summary>
    public class EventsReporter
    {
        private readonly AnalyticsReporter _reporter;

        public EventsReporter(AnalyticsReporter reporter)
        {
            _reporter = reporter;
        }

        public void ReportEventAsync(
            string eventType,
            string eventName,
            string projectId = null,
            DateTime? timestamp = null,
            Dictionary<string, string> metadata = null)
        {

        }

        private static string FormatPageView(string eventType, string eventName) =>
            $"/virtual/{eventType}/{eventName}";
    }
}
