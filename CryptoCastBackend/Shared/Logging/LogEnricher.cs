using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace Shared.Logging
{
    public class LogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var activity = Activity.Current;

            logEvent.AddPropertyIfAbsent(new LogEventProperty("TraceId", new ScalarValue(activity.GetTraceId())));
            logEvent.AddPropertyIfAbsent(new LogEventProperty("SpanId", new ScalarValue(activity.GetSpanId())));
            logEvent.AddPropertyIfAbsent(new LogEventProperty("ParentId", new ScalarValue(activity.GetParentId())));
        }
    }
}
