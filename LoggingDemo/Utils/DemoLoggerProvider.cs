using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace LoggingDemo.Utils
{
    public class DemoLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DemoLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName) => new DemoLogger(_testOutputHelper, categoryName);

        public void Dispose()
        { }
    }
}
