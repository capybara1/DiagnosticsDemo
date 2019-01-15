using DiagnosticsDemo.LoggingDemo.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace DiagnosticsDemo.LoggingDemo
{
    public class AdvancedDemos
    {
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public AdvancedDemos(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Log to Test Output Using Scope")]
        public void LogToTestOutputOutputUsingScope()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddDemo(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            logger.LogInformation("Before Scope");
            using (logger.BeginScope("Demo Scope"))
            {
                logger.LogInformation("In Scope");
            }
            logger.LogInformation("After Scope");
        }
    }
}
