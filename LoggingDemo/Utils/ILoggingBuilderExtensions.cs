using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace LoggingDemo.Utils
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper testOutputHelper)
        {
            builder.AddProvider(new XunitLoggerProvider(testOutputHelper));
            return builder;
        }
    }
}
