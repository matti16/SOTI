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

        public override string RedButtonText { get { return "Market"; } }
        public override string BlueButtonText { get { return "Recipe"; } }
    }
}