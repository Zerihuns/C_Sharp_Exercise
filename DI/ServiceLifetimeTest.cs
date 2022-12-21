using Microsoft.Extensions.DependencyInjection;
using System;

namespace DI
{
    static class ServiceLifetimeTest
    {
        public static void Run()
        {
            var serviceBuilder = new ServiceCollection();

            serviceBuilder
                .AddSingleton<SingletonDisposableService>()
                .AddTransient<TransientDisposableService>()
                .AddScoped<ScopedDisposableService>()
                ;

            // You should use validateScopes: true as a best practice. It's set to false here for demo purposes.
            using var services = serviceBuilder.BuildServiceProvider(validateScopes: false);

            var logger = new TestLogger();
            
            // Tests in Scope
            logger.Header("Creating a service scope...");
            using (var scope = services.CreateScope())
            {
                // Resolve scoped service twice and compare reference

                // Resolve scoped service first time
                logger.Header("Resolving first scoped service in service scope...");
                var scopedService1 = scope.ServiceProvider.GetService<ScopedDisposableService>();
                scopedService1.SayHello();
                logger.Separator();

                // Resolve scoped service second time
                logger.Header("Resolving second scoped service in service scope...");
                var scopedService2 = scope.ServiceProvider.GetService<ScopedDisposableService>();
                scopedService2.SayHello();
                logger.Separator();

                // Compare references of two scope services and print the result
                var scopedServicesAreSame = Object.ReferenceEquals(scopedService1, scopedService2);
                logger.Log($"{nameof(scopedService1)} { (scopedServicesAreSame ? "==" : "!=") } {nameof(scopedService2)}");

                // Resolve transient service
                logger.Header("Resolving transient service in service scope...");
                var transientInScope = scope.ServiceProvider.GetService<TransientDisposableService>();
                transientInScope.SayHello();
                logger.Separator();

                // Resolve sigleton service
                logger.Header("Resolving singleton service in service scope...");
                var singletonInScope = scope.ServiceProvider.GetService<SingletonDisposableService>();
                singletonInScope.SayHello();
                logger.Separator();
            }
            logger.Log("Service scope disposed.", spaceAfter: true);

            // Resolve tests in root scope
            logger.Header("Testing in root scope");

            // Singleton service
            logger.Log("Resolving singleton service in ROOT scope...");
            var singletonService = services.GetService<SingletonDisposableService>();
            singletonService.SayHello();
            logger.Separator();

            // Transient service
            logger.Log("Resolving transient service in ROOT scope...");
            var transientService = services.GetService<TransientDisposableService>();
            transientService.SayHello();
            logger.Separator();

            // Scoped service
            logger.Log("Resolving scope service in ROOT scope...");
            // If validateScopes parameter is true, [ ex: serviceBuilder.BuildServiceProvider(validateScopes: true) ]
            // This line will throw an exception, because scope services will not be allowed in the root scope.
            // In this test we used validateScopes: false for demo purposes.
            var scopedService = services.GetService<ScopedDisposableService>();
            scopedService.SayHello();
            logger.Separator();

            logger.Header("Disposing services...");

            // Although we used "using..." keyword as a best practice while building services, we explicitly dispose it for demo purposes
            services.Dispose();
            logger.Log("Services disposed.");
            logger.Separator();
            // Tests in Root
        }

    }
}
