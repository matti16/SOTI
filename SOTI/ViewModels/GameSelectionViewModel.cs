using Caliburn.Micro;
using SOTI.Message;
using SOTI.ViewModels.Recipe;
using System.Windows;

namespace SOTI.ViewModels
{
    public class GameSelectionViewModel : BaseGameScreenViewModel
    {
        public GameSelectionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public override void Handle(BlueButtonMessage message)
        {
            //Navigate to the ChooseRecipe Screen
            this.NavigateToScreen<ChooseRecipeViewModel>();
            base.Handle(message);
        }

        public override Visibility GreenButtonVisibility { get { return Visibility.Hidden; } }

        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }

        public override string BlueButtonText { get { return "Recipe"; } }
    }
}