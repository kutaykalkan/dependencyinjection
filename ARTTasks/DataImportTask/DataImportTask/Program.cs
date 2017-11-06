using System;
using System.Linq;
using DataImportTask.Implementations.CommandHandlers;
using DataImportTask.Implementations.Proxies;
using DataImportTask.Implementations.Services;
using DataImportTask.Interfaces;
using SimpleInjector;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Shared.Interfaces;
using SkyStem.ART.Shared.Proxies;
using SkyStem.ART.Shared.Utility;

namespace DataImportTask
{
    public class Program
    {
        private static readonly Container Container = new Container();

        private static void Main(string[] args)
        {
            ConfigureDIContainer();
            Container.Verify();
            Container.GetAllInstances<ICommandHandler>().SingleOrDefault(x => x.GetType() == typeof(GLDataImporterCommandHandler))?.Handle();

            Console.Read();
        }

        private static void ConfigureDIContainer()
        {
            //-------------------
            //Register NLogger proxy. Just request ILogger service from the consuming ctor as usual.
            Container.RegisterConditional(typeof(ILogger),
                context => typeof(NLogProxy<>).MakeGenericType(context.Consumer.ImplementationType),
                Lifestyle.Singleton, context => true);
            //---------
            //-------------------
            //Register all implementations of open generic ICommandHandler 
            Container.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies());
            //Register all implementations of ICommandHandler
            Container.RegisterCollection(typeof(ICommandHandler), AppDomain.CurrentDomain.GetAssemblies());
            //Manual Registration
            Container.Register<ICacheService, CacheService>();
            Container.Register<IAppSettingHelper, AppSettingHelper>();
            //---------------------

            //------------------
            //If implementation type of the consumer is RemotingHelperProxy, inject remoting helper.
            Container.RegisterConditional<IRemotingHelper, RemotingHelper>(
                c => c.Consumer.ImplementationType == typeof(RemotingHelperProxy));
            //Otherwise, inject ReomtingHelperProxy.
            Container.RegisterConditional<IRemotingHelper, RemotingHelperProxy>(
                c => !c.Handled);
            //------------------
        }
    }
}