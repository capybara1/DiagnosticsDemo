using LoggingDemo.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        [Fact(DisplayName = "Configure LogLevel Programatically")]
        public void ConfigureLogLevelProgramatically()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddFilter("LoggingDemo.Helper", LogLevel.Debug);
                builder.SetMinimumLevel(LogLevel.Information);

                builder.AddDemo(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();
            var otherLogger = serviceProvider.GetRequiredService<ILogger<Helper.OtherCategory>>();

            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            otherLogger.LogDebug("Debug in other category, located below LoggingDemo.Helper");
        }

        [Fact(DisplayName = "Configure LogLevel Programatically For Specific Provider")]
        public void ConfigureLogLevelProgramaticallyForSpecificProvider()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddFilter<DemoLoggerProvider>("LoggingDemo", LogLevel.Information);

                builder.AddDemo(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            logger.LogDebug("Debug");
            logger.LogInformation("Information");
        }

        [Fact(DisplayName = "Configure LogLevel Via Configuration")]
        public void ConfigureLogLevelViaConfiguration()
        {
            var loggingConfiguration = CreateConfiguration(new[]
            {
                new KeyValuePair<string, string>(
                    ConfigurationPath.Combine("LogLevel", "LoggingDemo.Helper"),
                    "Debug"),
                new KeyValuePair<string, string>(
                    ConfigurationPath.Combine("LogLevel", "Default"),
                    "Information"),
            });
            
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(loggingConfiguration);
                builder.AddDemo(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();
            var otherLogger = serviceProvider.GetRequiredService<ILogger<Helper.OtherCategory>>();

            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            otherLogger.LogDebug("Debug in other category, located below LoggingDemo.Helper");
        }

        [Fact(DisplayName = "Configure LogLevel Via Configuration For Specific Provider")]
        public void ConfigureLogLevelViaConfigurationForSpecificProvider()
        {
            var loggerName = Regex.Replace(nameof(DemoLoggerProvider), "Provider$", string.Empty);
            var loggingConfiguration = CreateConfiguration(new[]
            {
                new KeyValuePair<string, string>(
                    ConfigurationPath.Combine(loggerName, "LogLevel", "LoggingDemo"),
                    "Information"),
            });
            
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(loggingConfiguration);
                builder.AddDemo(_testOutputHelper);
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
