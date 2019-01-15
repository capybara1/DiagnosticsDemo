using Microsoft.Extensions.Logging;

namespace DiagnosticsDemo.LoggingDemo.Helper
{
    internal static class EventIds
    {
        public static readonly EventId DemoEvent = new EventId(1, "DemoEvent");
    }
}
