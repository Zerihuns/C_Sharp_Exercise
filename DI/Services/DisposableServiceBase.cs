using System;

namespace DI
{
    public abstract class DisposableServiceBase : IDisposable
    {
        public DisposableServiceBase()
        {
            Id = UniqueId.Next(GetType().FullName);
            LogWithColor($"{ServiceInstanceName} created.", ConsoleColor.DarkCyan);
        }

        public int Id { get; }

        static string GeneratePrettyName(string serviceName)
        {
            if (serviceName.EndsWith("DisposableService"))
                return serviceName.Substring(0, serviceName.Length - "DisposableService".Length);

            if (serviceName.EndsWith("Service"))
                return serviceName.Substring(0, serviceName.Length - "Service".Length);

            return serviceName;
        }
        public string ServiceInstanceName => $"{GeneratePrettyName(GetType().Name)}#{Id}";

        public void SayHello()
        {
            LogWithColor($"Hello from {ServiceInstanceName}!");
        }

        static void LogWithColor(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #region IDisposable 

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LogWithColor($"{ServiceInstanceName} disposed.", ConsoleColor.Red);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
