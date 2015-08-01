using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using ReachMailDriver.Services.Gateway;
using ReachMailDriver.Util.Aspects;
using System;

namespace ReachMailDriver.Services
{
    public sealed class ServiceFactory
    {
        private static readonly Lazy<ServiceFactory> lazy = new Lazy<ServiceFactory>(() => new ServiceFactory());

        private ServiceFactory() { }

        public static ServiceFactory Instance { get { return lazy.Value; } }

        public T getService<T>(IReachMailApiGateway apiGateway)
        {
            var proxyGenerator = new ProxyGenerator();

            return (T)proxyGenerator.CreateClassProxy(
               typeof(T), new object[] { apiGateway }, new IInterceptor[] { new ConsoleAspect() });
        }
    }
}
