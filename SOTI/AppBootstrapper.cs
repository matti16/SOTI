using Caliburn.Micro;
using System.Windows;

namespace SOTI
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
            //_container.PerRequest<MainViewModel>();
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //DisplayRootViewFor<MainViewModel>();
        }

    }
}
