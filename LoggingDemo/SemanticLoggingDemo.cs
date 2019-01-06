using System;
using System.Collections.Generic;
using System.Linq;
using LoggingDemo.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace LoggingDemo
{
    public class SemanticLoggingDemo
    {
        private class DemoEvent
        {
            public int SomeInformation { get; set; }

            public override string ToString() => "DemoEvent";
        }

        private class SemanticLoggerProvider : ILoggerProvider
        {
            private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

            public SemanticLoggerProvider(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
            }

            public ILogger CreateLogger(string categoryName) => new SemanticLogger(_testOutputHelper, categoryName);

            public void Dispose()
            { }
        }

        private class SemanticLogger : XunitLogger
        {
            private readonly System.Reflection.BindingFlags Flags = System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance;

            private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

            public SemanticLogger(Xunit.Abstractions.ITestOutputHelper testOutputHelper, string categoryName)
                : base(testOutputHelper, categoryName)
            {
                _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
            }

            public override void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception exception,
                Func<TState, Exception, string> formatter)
            {
                base.Log(logLevel, eventId, state, exception, formatter);

                var structure = state as IEnumerable<KeyValuePair<string, object>>;
                if (structure != null)
                {
                    foreach (var semanticMessage in structure.Where(kvp => kvp.Key.StartsWith('+')))
                    foreach (var property in semanticMessage.Value.GetType().GetProperties(Flags))
                    {
                        var value = property.GetValue(semanticMessage.Value, null);
                        _testOutputHelper.WriteLine($"{property.Name}: {value} ({value.GetType().Name})");
                    }
                }
            }
        }

        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public SemanticLoggingDemo(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using LoggerFactory")]
        public void LogToTestOutputOutputWithExtensionMethodUsingLoggerFactory()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddProvider(new SemanticLoggerProvider(_testOutputHelper));
            });

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<BasicDemos>();

            var @event = new DemoEvent
            {
                SomeInformation = 123,
            };
            logger.LogInformation("Event: {+message}", @event);
        }
    }
}
