namespace SOTI {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using ViewModels;
    using Model;
    using ViewModels.Recipe;
    using ViewModels.Market;
    using System.Dynamic;
    using System.Windows;
    public class AppBootstrapper : BootstrapperBase
    {
        public static readonly SimpleContainer container = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            /*

                IMPORTANTE
                Ricordarsi di aggiungere SEMPRE i vari ViewModel che vengono utilizzati nell'applicazione al container.
                In generale i servizi comuni a più ViewModel saranno tendenzialmente Singleton, mentre i singoli
                ViewModel saranno PerRequest

            */
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<DataLayer>();
            container.Singleton<SerialCommunication>();
            container.Singleton<StateRicetta>();
            container.Singleton<StateMarket>();
            container.Singleton<Speech>();

            container.PerRequest<MainViewModel>();
            container.PerRequest<GameSelectionViewModel>();
            //Recipe Game View Models
            container.PerRequest<ChooseRecipeViewModel>();
            container.PerRequest<RecipeStepViewModel>();
            container.PerRequest<AllergoloRecipeViewModel>();
            container.PerRequest<EndRecipeViewModel>();
            //Market Game View Models
            container.PerRequest<IntroViewModel>();
            container.PerRequest<AllergoloMarketViewModel>();
            container.PerRequest<PaymentViewModel>();
            container.PerRequest<WaitingCardViewModel>();
            container.PerRequest<WaitingProductsViewModel>();


        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.None;
            settings.WindowState = WindowState.Maximized;
            /*
            settings.Top = 0;
            settings.Left = 0;
            settings.Width = 1024;
            settings.Height = 768;
            */
            settings.ShowInTaskbar = false;
            settings.SizeToContent = SizeToContent.Manual;
            settings.Title = "SOTI";

            DisplayRootViewFor<MainViewModel>(settings);
        }
    }
}