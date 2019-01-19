using System;
using Xunit;

namespace EventSourceDemo
{
    public class BasicDemos
    {
        private static readonly Random RandomNumbers = new Random(0);

        [Fact(DisplayName = "Create Event")]
        public void CreateEvent()
        {
            DemoEventSource.Instance.SomeEvent("Demo Event");
        }

        [Fact(DisplayName = "Create event using additional parameters")]
        public void CreateEventUsingAdditionalParameters()
        {
            DemoEventSource.Instance.OtherEvent(
                RandomNumbers.Next(0, 100),
                100,
                0,
                50);
        }

        [Fact(DisplayName = "Change Event Counter")]
        public void ChangeEventCounter()
        {
            DemoEventSource.Instance.ChangeDemoCounter(RandomNumbers.Next());
        }
    }
}
