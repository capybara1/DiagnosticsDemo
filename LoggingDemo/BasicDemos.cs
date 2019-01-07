using LoggingDemo.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace LoggingDemo
{
    public class BasicDemos
    {
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public BasicDemos(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
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
            var logger = loggerFactory.CreateLogger<BasicDemos>();

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

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            logger.LogInformation("Hello World");
        }

        [Fact(DisplayName = "Log to Test Output With Extension Method Using Named Parameters")]
        public void LogToTestOutputOutputWithExtensionMethodUsingNamedParameters()
        {
            // Named parameters have been preferred over indices as e.g. used in System.String.Format,
            // in order to support providers with the capability of semantic/structured logging.
            // However, how the information is processed by the provider, is not defined.
            // Some providers such as e.g. Serilog or NLog use types and naming conventions in order
            // to control the interpretation of the values.
            
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddXunit(_testOutputHelper);
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();
            
            logger.LogInformation("Simple: {greeting}", "Hello World");
            logger.LogInformation("Object: {event}", new Helper.DemoEvent { Data = 1 });
            logger.LogInformation("Anonymous Object: {data}", new { id = 1 });
            logger.LogInformation("Dictionary: {data}", new Dictionary<string, object> { ["loc"] = "here", ["id"] = 1 });
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

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

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

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

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

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            Helper.DemoEvent.Emit(logger, 123);
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

            var logger = serviceProvider.GetRequiredService<ILogger<BasicDemos>>();

            var log = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1, "Demo"),
                "Hello World");

            var exception = new InvalidOperationException();
            Helper.DemoEvent.Emit(logger, 123, exception);
        }
    }
}
