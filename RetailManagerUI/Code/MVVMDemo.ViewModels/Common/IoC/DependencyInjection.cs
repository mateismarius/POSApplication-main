using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Internal.DataAccess;
using DataAccesLibrary.Internal.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.Logging;
using RetailManagerUI.ViewModels.Common.MessageBox;
using RetailManagerUI.ViewModels.Common.MVVM;
using RetailManagerUI.ViewModels.Dispatching;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Abstractions;

namespace RetailManagerUI.ViewModels.Common.IoC
{
    public class DependencyInjection
    {
        private static IServiceProvider serviceProvider;
        private static readonly IServiceCollection services;
        private static readonly DependencyInjection instance = new DependencyInjection();

        public static IServiceProvider ServiceProvider
        {
            get { return serviceProvider; }
        }

        public static DependencyInjection Instance
        {
            get { return instance; }
        }

        static DependencyInjection()
        {
            services = new ServiceCollection();
            // add the custom services to the service collection
            Instance.RegisterCustomServices();
            // add dispatcher services
            Instance.RegisterDispatcherServices();
            // add dialog services
            Instance.RegisterDialogServices();
        }

        private void RegisterCustomServices()
        {
            services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));
            // for MySQL databases, install MySQL.Data:
            // services.AddTransient<ISqlDataAccess, MySqlConnection>();
            // for SQLite databases, install System.Data.SQLite.Core:
            // services.AddTransient<ISqlDataAccess, SQLiteConnection>();
            // for MSSQL Server databases, use System.Data.SqlClient
            services.AddSingleton<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IDbConnection, SqlConnection>();
            services.AddTransient<IStartUpData, StartUpData>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IInvoiceData, InvoiceData>();
            services.AddTransient<IUserData, UserData>();
            services.AddTransient<ISalesData, SalesData>();
        }

        private void RegisterDialogServices()
        {
            // depending on the specified automation env, register a specific dialog service
            switch (BaseModel.testAutomationEnvironment)
            {
                case TestAutomationEnvironment.None:
                    services.AddTransient<IMessageBoxService, MessageBoxService>();
                    break;
                case TestAutomationEnvironment.Console:
                    services.AddTransient<IMessageBoxService, ConsoleService>();
                    break;
                case TestAutomationEnvironment.NLog:
                    services.AddTransient<IMessageBoxService, LoggingService>();
                    break;
            }
        }

        public void RegisterDispatcherServices(Type dispatcher = null)
        {
            // if no dispatcher is provided, such as in testing env, add a mockup dispatcher
            if (dispatcher == null)
                services.AddTransient<IDispatcher, MockDispatcher>();
            else
            {
                // if a specific dispatcher was provided, check if a dispatcher was already registered
                ServiceDescriptor descriptor = new ServiceDescriptor(typeof(IDispatcher), dispatcher, ServiceLifetime.Transient);
                // if a dispatcher service was already registred, replace it with the new dispatcher provided
                if (descriptor != null)
                    services.Replace(descriptor);
                else
                    services.AddTransient(typeof(IDispatcher), dispatcher);
            }
            BuildServiceProvider();
        }

        internal void BuildServiceProvider()
        {
            serviceProvider = services.BuildServiceProvider();
        }

        internal void InjectServiceProvider(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
    }
}
