using System;
using System.Diagnostics;
using Xunit;

namespace DiagnosticsDemo.PerformanceCounterDemo
{
    public class BasicDemos
    {
        private const string TestCategory = "DiagnosticsDemo";
        private const string NumberOfItemsName = "DiagnosticsDemo1";

        [Fact(DisplayName = "NumberOfItems Demo")]
        public void NumberOfItemsDemo()
        {
            using (var counter = new PerformanceCounter(TestCategory, NumberOfItemsName, ""))
            {
                counter.ReadOnly = false;
                counter.IncrementBy(1);
            }
        }
    }
}