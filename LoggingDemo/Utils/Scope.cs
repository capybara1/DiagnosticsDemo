using System;

namespace LoggingDemo.Utils
{
    internal class Scope : IDisposable
    {
        private readonly DemoLogger _logger;

        public Scope(DemoLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void Dispose()
        {
            _logger.CurrentScope = null;
        }
    }
}
