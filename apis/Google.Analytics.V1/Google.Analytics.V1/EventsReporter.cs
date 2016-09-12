using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Google.Analytics.V1
{
    /// <summary>
    /// Reports events using the provided analytics reporter.
    /// </summary>
    public class EventsReporter
    {
        private const int ProjectIdHashIndex = 11;

        private readonly AnalyticsReporter _reporter;

        public EventsReporter(AnalyticsReporter reporter)
        {
            _reporter = Preconditions.CheckNotNull(reporter, nameof(reporter));
        }

        public void ReportEventAsync(
            string eventType,
            string eventName,
            string projectId = null,
            DateTime? timestamp = null,
            Dictionary<string, string> metadata = null)
        {
            Preconditions.CheckNotNull(eventType, nameof(eventType));
            Preconditions.CheckNotNull(eventName, nameof(eventName));

            _reporter.ReportPageView(
                page: GetPageViewURI(eventType: eventType, eventName: eventName),
                title: SerializeEventMetadata(metadata),
                customDimensions: new Dictionary<int, string>
                {
                    { ProjectIdHashIndex, GetProjectHash(projectId) }
                });

        }

        private static string GetProjectHash(string projectId)
        {
            var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(projectId));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        private static string SerializeEventMetadata(Dictionary<string, string> metadata)
        {
            return String.Join(",", metadata.Select(SerializeMetadataEntry));
        }

        private static string SerializeMetadataEntry(KeyValuePair<string, string> entry) =>
            $"{entry.Key}={EscapeValue(entry.Value)}";

        private static string EscapeValue(string value)
        {
            var result = new StringBuilder();
            foreach (var c in value)
            {
                switch (c)
                {
                    case ',':
                    case '=':
                    case '\\':
                        result.Append($@"\{c}");
                        break;

                    default:
                        result.Append(c);
                        break;
                }
            }
            return result.ToString();
        }

        private static string GetPageViewURI(string eventType, string eventName) =>
            $"/virtual/{eventType}/{eventName}";
    }
}
