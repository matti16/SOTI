namespace SOTI {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using ViewModels;
    using Model;
    using ViewModels.Recipe;
    using ViewModels.Market;

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

            container.PerRequest<MainViewModel>();
            container.PerRequest<GameSelectionViewModel>();
            //Recipe Game View Models
            container.PerRequest<ChooseRecipeViewModel>();
            container.PerRequest<RecipeStepViewModel>();
            container.PerRequest<AllergoloRecipeViewModel>();
            container.PerRequest<EndRecipeViewModel>();
            //Market Game View Models
            container.PerRequest<IntroViewModel>();

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
            DisplayRootViewFor<MainViewModel>();
        }
    }
}