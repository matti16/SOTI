using Caliburn.Micro;
using SOTI.Message;
using SOTI.ViewModels.Market;
using SOTI.ViewModels.Recipe;
using System.Windows;

namespace SOTI.ViewModels
{
    public class GameSelectionViewModel : BaseGameScreenViewModel
    {
        public GameSelectionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.HelpMessage = "Scegli un gioco premendo il pulsante.";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            switch (AppBootstrapper.startupGame)
            {
                case "--market":
                    this.eventAggregator.PublishOnBackgroundThread((new NavigationMessage(AppBootstrapper.container.GetInstance<IntroViewModel>())));
                    break;
                case "--recipe":
                    this.eventAggregator.PublishOnBackgroundThread((new NavigationMessage(AppBootstrapper.container.GetInstance<ChooseRecipeViewModel>())));
                    break;
                default:
                    break;
            }
        }

        public override async void Handle(BlueButtonMessage message)
        {
            //Navigate to the ChooseRecipe Screen
            await this.NavigateToScreen<ChooseRecipeViewModel>();
            base.Handle(message);
        }

        public override async void Handle(RedButtonMessage message)
        {
            //Navigate to the ChooseRecipe Screen
            await this.NavigateToScreen<IntroViewModel>();
            base.Handle(message);
        }

        public override Visibility GreenButtonVisibility { get { return Visibility.Hidden; } }

        public override string RedButtonText { get { return "Spesa"; } }
        public override string BlueButtonText { get { return "Ricetta"; } }
        
    }
}