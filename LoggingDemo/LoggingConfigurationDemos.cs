using LoggingDemo.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using Xunit;


namespace LoggingDemo
{
    public class LoggingConfigurationDemos
    {
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public LoggingConfigurationDemos(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Configure Default LogLevel Programatically")]
        public void ConfigureDefaultLogLevelProgramatically()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);

                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            logger.LogDebug("Debug");
            logger.LogInformation("Information");
        }

        [Fact(DisplayName = "Configure Default LogLevel Via Configuration")]
        public void ConfigureDefaultLogLevelViaConfiguration()
        {
            var loggingConfiguration = CreateConfiguration(new[]
            {
                new KeyValuePair<string, string>("Logging::LogLevel::Default", "Information"),
            });
            
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(loggingConfiguration);
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            logger.LogDebug("Debug");
            logger.LogInformation("Information");
        }

        private static IConfiguration CreateConfiguration(IEnumerable<KeyValuePair<string, string>> configurationValues)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(configurationValues);
            var result = configurationBuilder.Build();

            return result;
        }
    }
}
