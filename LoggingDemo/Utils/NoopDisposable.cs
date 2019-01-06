using System;

namespace LoggingDemo.Utils
{
    internal class NoopDisposable : IDisposable
    {
        public static readonly NoopDisposable Instance = new NoopDisposable();

        public void Dispose()
        { }
    }
}
