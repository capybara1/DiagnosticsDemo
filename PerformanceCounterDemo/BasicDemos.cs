using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace DiagnosticsDemo.PerformanceCounterDemo
{
    public class BasicDemos
    {
        private const double DefaultDuration = 3;

        // Average: measure a value over time and display the average of
        // the last two measurements.Associated with each average counte
        // is a base counter that tracks the number of samples involved.

        // Difference: subtract the last measurement from the previous one and
        // display the difference, if it is positive; if negative, they display a zero.

        // Instantaneous: display the most recent measurement.

        // Percentage: display calculated values as a percentage.

        // Rate: sample an increasing count of events over time and divide the change
        // in count values by the change in time to display a rate of activity.

        private const string TestCategoryName = "DiagnosticsDemo";
        private const string NumberOfItemsName = "NumberOfItems64";
        private const string CounterDeltaName = "CounterDelta64";
        private const string CounterTimerName = "CounterTimer";
        private const string CounterTimerInverseName = "CounterTimerInverse";
        private const string CountPerTimeIntervalName = "CountPerTimeInterval64";
        private const string RateOfCountsPerSecondName = "RateOfCountsPerSecond64";
        private const string ElapsedTimeName = "ElapsedTime";
        private const string RawFractionName = "RawFraction";
        private const string RawFractionBaseName = "RawFractionBase";
        private const string SampleFractionName = "SampleFraction";
        private const string SampleFractionBaseName = "SampleFractionBase";
        private const string AverageCountName = "AverageCount64";
        private const string AverageCountBaseName = "AverageCount64Base";
        private const string AverageTimerName = "AverageTimer32";
        private const string AverageTimerBaseName = "AverageTimer32Base";

        private static readonly Random RandomNumbers = new Random(0);

        public BasicDemos()
        {
            if (!PerformanceCounterCategory.Exists(TestCategoryName))
            {
                Debug.WriteLine($"The category {TestCategoryName} does not exist");
            }
        }

        [Fact(DisplayName = NumberOfItemsName + " Demo")]
        public void NumberOfItemsDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, NumberOfItemsName, ""))
            {
                counter.ReadOnly = false;
                counter.Increment();
            }
        }

        [Fact(DisplayName = CounterDeltaName + " Demo")]
        public async Task CounterDeltaDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, CounterDeltaName, ""))
            {
                counter.ReadOnly = false;
                await DoWork(() => counter.Increment());
            }
        }

        [Fact(DisplayName = CounterTimerName + " Demo")]
        public async Task CounterTimerDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, CounterTimerName, ""))
            {
                counter.ReadOnly = false;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    await DoWork();
                }
                finally
                {
                    stopwatch.Stop();
                    counter.IncrementBy(stopwatch.ElapsedTicks);
                }
            }
        }

        [Fact(DisplayName = CounterTimerInverseName + " Demo")]
        public async Task CounterTimerInverseDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, CounterTimerInverseName, ""))
            {
                counter.ReadOnly = false;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    await DoWork();
                }
                finally
                {
                    stopwatch.Stop();
                    counter.IncrementBy(stopwatch.ElapsedTicks);
                }
            }
        }

        [Fact(DisplayName = CountPerTimeIntervalName + " Demo")]
        public async Task CountPerTimeIntervalDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, CountPerTimeIntervalName, ""))
            {
                counter.ReadOnly = false;
                await DoWork(() => counter.Increment());
            }
        }

        [Fact(DisplayName = RateOfCountsPerSecondName + " Demo")]
        public async Task RateOfCountsPerSecondDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, RateOfCountsPerSecondName, ""))
            {
                counter.ReadOnly = false;
                await DoWork(() => counter.Increment());
            }
        }

        [Fact(DisplayName = RawFractionName + " Demo")]
        public void RawFractionDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, RawFractionName, ""))
            using (var baseCounter = new PerformanceCounter(TestCategoryName, RawFractionBaseName, ""))
            {
                counter.ReadOnly = false;
                baseCounter.ReadOnly = false;
                counter.IncrementBy(1);
                baseCounter.IncrementBy(1);
            }
        }

        [Fact(DisplayName = SampleFractionName + " Demo")]
        public void SampleFractionDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, SampleFractionName, ""))
            using (var baseCounter = new PerformanceCounter(TestCategoryName, SampleFractionBaseName, ""))
            {
                counter.ReadOnly = false;
                baseCounter.ReadOnly = false;
                if (RandomNumbers.Next()%3 == 0)
                {
                    counter.IncrementBy(1);
                }
                baseCounter.IncrementBy(1);
            }
        }

        [Fact(DisplayName = AverageCountName + " Demo")]
        public void AverageCountDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, AverageCountName, ""))
            using (var baseCounter = new PerformanceCounter(TestCategoryName, AverageCountBaseName, ""))
            {
                counter.ReadOnly = false;
                baseCounter.ReadOnly = false;
                if (RandomNumbers.Next() % 3 == 0)
                {
                    counter.IncrementBy(1);
                }
                baseCounter.IncrementBy(1);
            }
        }

        [Fact(DisplayName = AverageTimerName + " Demo")]
        public async Task AverageTimerDemo()
        {
            using (var counter = new PerformanceCounter(TestCategoryName, AverageTimerName, ""))
            using (var baseCounter = new PerformanceCounter(TestCategoryName, AverageTimerBaseName, ""))
            {
                counter.ReadOnly = false;
                baseCounter.ReadOnly = false;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    await DoWork();
                }
                finally
                {
                    stopwatch.Stop();
                    counter.IncrementBy(stopwatch.ElapsedTicks);
                    baseCounter.IncrementBy(1);
                }
            }
        }

        private static async Task DoWork(double seconds = DefaultDuration)
        {
            var duration = TimeSpan.FromSeconds(seconds);
            await Task.Delay(duration);
        }

        private static async Task DoWork(Action action, double seconds = DefaultDuration)
        {
            var maxTotalDuration = TimeSpan.FromSeconds(seconds);
            var actualTotalDuration = TimeSpan.Zero;
            while (actualTotalDuration >= maxTotalDuration)
            {
                action();
                var delayDuration = TimeSpan.FromMilliseconds(RandomNumbers.Next(200, 500));
                if (actualTotalDuration >= maxTotalDuration)
                {
                    delayDuration = maxTotalDuration - actualTotalDuration;
                };
                await Task.Delay(delayDuration);
                actualTotalDuration += delayDuration;
            }
        }
    }
}
