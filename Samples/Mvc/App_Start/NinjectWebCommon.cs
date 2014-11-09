[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Vsxtend.Samples.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Vsxtend.Samples.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace Vsxtend.Samples.Mvc.App_Start
{
    using System;
    using System.Web;
    using System.Linq;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Vsxtend.Common;
    using Vsxtend.Interfaces;
    using Vsxtend.Resources;
    using Microsoft.AspNet.SignalR;
    using System.Collections.Generic;
    using Vsxtend.Samples.Mvc.Services;
    using System.Configuration;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            BasicAuthentication auth = new BasicAuthentication { Username = "your_alt_credentials", Password = "your_password", Account = "your_account.visualstudio.com/DefaultCollection" };
            kernel.Bind<ITeamRoomClient>().To<TeamRoomClient>().WithConstructorArgument("authentication", auth);
            kernel.Bind<IRestHttpClient>().To<RestHttpClient>();
            kernel.Bind<ITeamRoomService>().To<TeamRoomService>();
            OAuthAuthorization oauth = new OAuthAuthorization { Account = ConfigurationManager.AppSettings["VisualStudioOnlineAccount"].ToString() };
            kernel.Bind<IOAuthUtility>().To<OAuthUtility>().WithConstructorArgument("authentication", oauth); ;
        }        
    }

    public static class NinjectIoC 
    { 
        public static IKernel Initialize() 
        { 
            IKernel kernel = new StandardKernel();

            BasicAuthentication auth = new BasicAuthentication { Username = "your_alt_credentials", Password = "your_password", Account = "your_account.visualstudio.com/DefaultCollection" };
            kernel.Bind<ITeamRoomClient>().To<TeamRoomClient>().WithConstructorArgument("authentication", auth);
            kernel.Bind<IRestHttpClient>().To<RestHttpClient>();
            kernel.Bind<ITeamRoomService>().To<TeamRoomService>();
            OAuthAuthorization oauth = new OAuthAuthorization { Account = ConfigurationManager.AppSettings["VisualStudioOnlineAccount"].ToString() };
            kernel.Bind<IOAuthUtility>().To<OAuthUtility>().WithConstructorArgument("authentication", oauth);

            return kernel; 
        } 
    }

    internal class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}
