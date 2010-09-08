using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Netduino.Core.ViewModels;
using Netduino.Core.Services;
using System.IO;
using Microsoft.SPOT.Tasks;
using System.Xml;

namespace Netduino.Core
{
    /// <summary>
    /// The bootstrapper for initializing the application
    /// </summary>
	public class EmulatorBootstrapper : Bootstrapper<IShellViewModel>
	{
		private CompositionContainer container;

        /// <summary>
        /// Configure the IOC Container and get the emulator started
        /// </summary>
        protected override void Configure()
		{
			LogManager.GetLog = type => new DebugLogger(type);

            AggregateCatalog catalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());
			container = new CompositionContainer(catalog);

			var batch = new CompositionBatch();

			batch.AddExportedValue<IWindowManager>(new WindowManager());

            IEventAggregator aggregator = new EventAggregator();
			batch.AddExportedValue<IEventAggregator>(aggregator);
			
            EmulatorService service = new EmulatorService(aggregator);
			batch.AddExportedValue<IEmulatorService>(service);
			
            batch.AddExportedValue(container);

			container.Compose(batch);

            // Start the emulator
			service.StartEmulator();
		}

        /// <summary>
        /// Resolve an instance of a contract
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
		protected override object GetInstance(Type serviceType, string key)
		{
			string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = container.GetExportedValues<object>(contract);

			if (exports.Count() > 0)
				return exports.First();

			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override void BuildUp(object instance)
		{
			container.SatisfyImportsOnce(instance);
		}
		
        /// <summary>
        /// Determine what assemblies are part of the application
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
		{
            return new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Extensions\NetduinoEmulator.dll")) };
        }
		
        /// <summary>
        /// Display the main windows
        /// </summary>
        protected override void DisplayRootView()
		{
			var viewModel = IoC.Get<IShellViewModel>();
			new WindowManager().Show(viewModel);
		}  
	}
}
