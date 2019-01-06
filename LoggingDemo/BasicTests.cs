using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using LoggingDemo.Utils;
using System;

namespace LoggingDemo
{
    public class BasicTests
    {
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public BasicTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new System.ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using LoggerFactory")]
        public void LogToTestOutputOutputWithExtensionMethodUsingLoggerFactory()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<BasicTests>();

            logger.LogInformation("Hello World");
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using Logger")]
        public void LogToTestOutputOutputWithExtensionMethodUsingLogger()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            logger.LogInformation("Hello World");
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using Parameters")]
        public void LogToTestOutputOutputWithExtensionMethodUsingParameters()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            logger.LogInformation("Hello {name}", "World");
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using Exception")]
        public void LogToTestOutputOutputWithExtensionMethodUsingException()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            var exception = new InvalidOperationException();
            logger.LogInformation("Hello World", exception);
        }

        [Fact(DisplayName = "Log to Test Output With LoggerMessage")]
        public void LogToTestOutputOutputWithLoggerMessage()
        {
            // See also https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage?view=aspnetcore-2.2

            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            var log = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1, "Demo"),
                "Hello World");
            
            log(logger, null);
        }

        [Fact(DisplayName = "Log to Test Output With LoggerMessage Using Parameters")]
        public void LogToTestOutputOutputWithLoggerMessageUsingParameters()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            var log = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(1, "Demo"),
                "Hello {name}");

            log(logger, "World", null);
        }

        [Fact(DisplayName = "Log to Test Output With LoggerMessage Using Exception")]
        public void LogToTestOutputOutputWithLoggerMessageUsingException()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicTests>>();

            var log = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1, "Demo"),
                "Hello World");

            var exception = new InvalidOperationException();
            log(logger, exception);
        }
    }
}
