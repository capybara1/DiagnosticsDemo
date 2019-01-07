using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace LoggingDemo.Utils
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddDemo(this ILoggingBuilder builder, ITestOutputHelper testOutputHelper)
        {
            builder.AddProvider(new DemoLoggerProvider(testOutputHelper));
            return builder;
        }
    }
}
