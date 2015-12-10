using System.Windows;
using Caliburn.Micro;
using SOTI.Message;

namespace SOTI.ViewModels.Recipe
{
    public class ChooseRecipeViewModel : BaseGameScreenViewModel
    {
        /// <summary>
        /// Costrutto della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public ChooseRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public override Visibility GreenButtonVisibility { get { return Visibility.Hidden; } }

        public override void Handle(RedButtonMessage message)
        {
            //Navigate to the RecipeStep Screen
            this.NavigateToScreen<RecipeStepViewModel>();
            base.Handle(message);
        }
    }
}
