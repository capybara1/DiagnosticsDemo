using System;

namespace LoggingDemo.Utils
{
    internal class Scope : IDisposable
    {
        private readonly XunitLogger _logger;

        public Scope(XunitLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void Dispose()
        {
            _logger.CurrentScope = null;
        }
    }
}
