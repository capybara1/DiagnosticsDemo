using Microsoft.Extensions.Logging;
using System;

namespace LoggingDemo.Helper
{
    internal class DemoEvent
    {
        private static readonly Action<ILogger, DemoEvent, Exception> _emit = LoggerMessage.Define<DemoEvent>(
            LogLevel.Information,
            EventIds.DemoEvent,
            "Event: {event}");

        public int Data { get; set; }
        
        public static void Emit(ILogger logger, int data)
        {
            Emit(logger, data, null);
        }

        public static void Emit(
            ILogger logger,
            int data,
            Exception exception)
        {
            var @event = new DemoEvent
            {
                Data = data,
            };
            _emit(logger, @event, null);
        }

        public override string ToString() => $"<String representation of DemoEvent {{ Data = {Data} }}>";
    }
}
