using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;

namespace LoggingDemo.Utils
{
    public class XunitLogger : ILogger
    {
        private static readonly BindingFlags GetPropertiesFlags = BindingFlags.Public | BindingFlags.Instance;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _categoryName;

        public XunitLogger(ITestOutputHelper testOutputHelper, string categoryName)
        {
            _testOutputHelper = testOutputHelper;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => NoopDisposable.Instance;

        public bool IsEnabled(LogLevel logLevel) => true;

        public virtual void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _testOutputHelper.WriteLine($"{_categoryName} [{eventId}] {formatter(state, exception)}");

            if (exception != null)
            {
                _testOutputHelper.WriteLine(exception.ToString());
            }
            
            var structure = state as IEnumerable<KeyValuePair<string, object>>;
            if (structure != null)
            {
                var semanticData = structure.Where(kvp => kvp.Key.StartsWith('@'))
                    .ToArray();
                if (semanticData.Length > 0)
                {
                    _testOutputHelper.WriteLine("Values that may be send to a value store:");

                    foreach (var item in semanticData)
                    foreach (var property in item.Value.GetType().GetProperties(GetPropertiesFlags))
                    {
                        var value = property.GetValue(item.Value, null);
                        _testOutputHelper.WriteLine($"{property.Name}: {value} ({value.GetType().Name})");
                    }
                }
            }
        }
    }
}
