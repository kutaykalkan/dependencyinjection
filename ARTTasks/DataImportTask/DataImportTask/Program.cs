using System;
using System.Linq;
using DataImportTask.Implementations.CommandHandlers;
using SimpleInjector;
using SkyStem.ART.Client.Interfaces;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Service.Interfaces;
using SkyStem.ART.Service.Proxies;
using SkyStem.ART.Service.Service;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Shared.Decorators;
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
            try
            {
                ConfigureDIContainer();

                //Dont run in production code. Run during debugging when needed.
                //It is recursing down the object tree and is a costly operation.
                //Container.Verify();

                //Below example is used when there are multiple implementations.
                //This retuns the TimeMeasuringCommandHandler that wraps 
                //the GLDataImporterCommandHandler.
                Container.GetAllInstances<BaseCommandHandler>().Single(o =>
                    o.GetType() == typeof(TimeMeasuringCommandHandlerDecorator)
                    && o.DecoratedType == typeof(GLDataImporterCommandHandler))?.Handle();
            }
            catch (ActivationException ex)
            {
                new NLogProxy<Program>().LogError(ex, "Problem with DI configuration!");
            }
            catch (Exception ex)
            {
                new NLogProxy<Program>().LogError(ex, ex.Message);
            }
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
            Container.RegisterCollection(typeof(BaseCommandHandler), AppDomain.CurrentDomain.GetAssemblies());
            //Container.RegisterCollection(typeof(ITimeMeasuringCommandHandlerDecorator), AppDomain.CurrentDomain.GetAssemblies());
            //Manual Registration
            Container.Register<ICacheService, CacheService>(Lifestyle.Singleton);
            Container.Register<IAppSettingHelper, AppSettingHelper>();
            Container.Register<IGLDataImport, GLDataImport>();
            Container.Register<IDataImportHelper, DataImportHelperProxy>();
            //---------------------

            //---------------------
            //If implementation type of the consumer is RemotingHelperProxy, inject remoting helper.
            Container.RegisterConditional<IRemotingHelper, RemotingHelper>(Lifestyle.Singleton,
                c => c.Consumer.ImplementationType == typeof(RemotingHelperProxy));
            //Otherwise, inject ReomtingHelperProxy.
            Container.RegisterConditional<IRemotingHelper, RemotingHelperProxy>(Lifestyle.Singleton, c => !c.Handled);
            //------------------

            //Decoration
            //Below wraps all BaseCommandHandlers with TimeMesuringCommandHandlerDecorator
            Container.RegisterDecorator(typeof(BaseCommandHandler), typeof(TimeMeasuringCommandHandlerDecorator));
            //---------------------
        }
    }
}