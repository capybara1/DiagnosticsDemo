using LoggingDemo.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace LoggingDemo
{
    public class SemanticLoggingDemo
    {
        // Note:
        // This demo relies on the capability of a logging-provider
        // to interpret parameters as structured data. Since this is not an
        // inbuild feature of Microsoft.Extensions.Logging.Abstractions,
        // semantic logging in this context requires other means,
        // such as naming conventions, to distinguish structured data
        // from mere formatting parameters.
        // In this demo the XunitLogger is designed to handle parameters,
        // keyed with names that begin with '@', as structured data. Keep
        // in mind that the solution, implemented forother frameworks,
        // might handle this differently.

        private class DemoEvent
        {
            private static readonly Action<ILogger, DemoEvent, Exception> _emit = LoggerMessage.Define<DemoEvent>(
                LogLevel.Information,
                new EventId(1, "DemoEvent"),
                "Event: {@event}");

            public int SomeInformation { get; set; }

            public static void Emit(ILogger logger, int someInformation)
            {
                var @event = new DemoEvent
                {
                    SomeInformation = someInformation,
                };
                _emit(logger, @event, null);
            }

            public override string ToString() => "DemoEvent";
        }

        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public SemanticLoggingDemo(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Log to Test Output Using Semantic Logging")]
        public void LogToTestOutputOutputUsingSemanticLogging()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<BasicDemos>();

            DemoEvent.Emit(logger, 123);
        }
    }
}
