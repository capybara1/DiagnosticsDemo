using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace DiagnosticsDemo.LoggingDemo.Utils
{
    public class DemoLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _categoryName;

        public string CurrentScope { get; set; }

        public DemoLogger(ITestOutputHelper testOutputHelper, string categoryName)
        {
            _testOutputHelper = testOutputHelper;
            _categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state)
        {
            CurrentScope = state.ToString();
            return new Scope(this);
        }

        public virtual void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var message = CurrentScope != null
                ? $"{CurrentScope}: {_categoryName} [{eventId}] {formatter(state, exception)}"
                : $"{_categoryName} [{eventId}] {formatter(state, exception)}";
            _testOutputHelper.WriteLine(message);

            if (exception != null)
            {
                _testOutputHelper.WriteLine(exception.ToString());
            }
            
            var structure = (state as IEnumerable<KeyValuePair<string, object>>)
                ?.Where(i => i.Key != "{OriginalFormat}")
                .ToArray();
            if (structure?.Length > 0)
            {
                _testOutputHelper.WriteLine("  Values that may be send to a value store:");

                foreach (var item in structure)
                {
                    var name = item.Key;
                    var value = item.Value ?? "null";
                    var type = item.Value?.GetType().FullName ?? "unknown type";
                    _testOutputHelper.WriteLine($" - {name}: {value} (of {type})");
                }
            }
        }
    }
}
