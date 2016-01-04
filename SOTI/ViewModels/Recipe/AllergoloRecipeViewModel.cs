using System;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System.Windows;

namespace SOTI.ViewModels.Recipe
{
    public class AllergoloRecipeViewModel : BaseGameScreenViewModel, IHandle<GreenButtonMessage>, IHandle<GUIReadyMessage>
    {
        private readonly StateRicetta state;

        public AllergoloRecipeViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.HelpMessage = "Ascolta il Dr. Allergolo. Poi premi Verde per continuare.";
            this.state = state;
        }

        public void Handle(GUIReadyMessage message)
        {
            bool doble = state.PassoCorrente.passoDoppio;
            if (doble)
            {
                Allergia allergia = state.Allergia;
                this.eventAggregator.PublishOnUIThread(new AllergoloAllergiaMessage(allergia));
            }
            else
            {
                Cibo ingredient = state.PassoCorrente.ciboGiusto;
                this.eventAggregator.PublishOnUIThread(new AllergoloCiboMessage(ingredient));
            }
        }

        public override void Handle(GreenButtonMessage message)
        {
            //Navigate to the RecipeStep Screen
            this.NavigateToScreen<RecipeStepViewModel>();
            base.Handle(message);
        }


        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }
        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }

    }
}