namespace DI
{
    public class ScopedDisposableService : DisposableServiceBase
    {
        public ScopedDisposableService(TransientDisposableService transientDisposableService, SingletonDisposableService singletonDisposableService)
        {
            TransientDisposableService = transientDisposableService;
            SingletonDisposableService = singletonDisposableService;
        }

        public TransientDisposableService TransientDisposableService { get; }
        public SingletonDisposableService SingletonDisposableService { get; }
    }
}
