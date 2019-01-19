using System;
using System.Diagnostics.Tracing;

namespace EventSourceDemo
{
    [EventSource(Name = "Demo-EventSource")]
    internal sealed class DemoEventSource : EventSource
    {
        // EventSource:
        // https://msdnshared.blob.core.windows.net/media/MSDNBlogsFS/prod.evol.blogs.msdn.com/CommunityServer.Components.PostAttachments/00/10/44/08/22/_EventSourceUsersGuide.docx
        // https://blogs.msdn.microsoft.com/vancem/2012/07/09/introduction-tutorial-logging-etw-events-in-c-system-diagnostics-tracing-eventsource/
        // https://blogs.msdn.microsoft.com/vancem/2012/08/13/windows-high-speed-logging-etw-in-c-net-using-system-diagnostics-tracing-eventsource/
        // http://geekswithblogs.net/BlackRabbitCoder/archive/2012/01/12/c.net-little-pitfalls-stopwatch-ticks-are-not-timespan-ticks.aspx
        // https://blogs.msdn.microsoft.com/ntdebugging/2009/09/08/part-2-exploring-and-decoding-etw-providers-using-event-log-channels/

        // EventCounter:
        // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.Tracing/documentation/EventCounterTutorial.md

        // PerfView:
        // https://github.com/Microsoft/perfview

        private readonly EventCounter _counter;

        public static DemoEventSource Instance { get; } = new DemoEventSource();

        public DemoEventSource()
        {
            _counter = new EventCounter("DemoCounter", this);
        }

        [Event(EventIds.SomeEvent, Level = EventLevel.Informational)]
        public void SomeEvent(string message)
        {
            Instance.WriteEvent(
                EventIds.SomeEvent,
                message);
        }

        [Event(EventIds.OtherEvent, Level = EventLevel.Informational)]
        public void OtherEvent(int actualValue, int maxValue, int minValue, int averageValue)
        {
            Instance.WriteEvent(
                EventIds.OtherEvent,
                actualValue,
                actualValue,
                actualValue,
                actualValue);
        }

        [NonEvent]
        public unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3, int arg4)
        {
            EventData* data = stackalloc EventData[4];

            data[0].DataPointer = (IntPtr)(&arg1);
            data[0].Size = 4;
            data[1].DataPointer = (IntPtr)(&arg2);
            data[1].Size = 4;
            data[2].DataPointer = (IntPtr)(&arg3);
            data[2].Size = 4;
            data[3].DataPointer = (IntPtr)(&arg4);
            data[3].Size = 4;

            WriteEventCore(eventId, 4, data);
        }

        public void ChangeDemoCounter(float metric)
        {
            _counter.WriteMetric(metric);
        }
    }
}
