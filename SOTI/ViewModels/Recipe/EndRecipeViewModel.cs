using System;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System.Windows;

namespace SOTI.ViewModels.Recipe
{
    public class EndRecipeViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private Ricetta recipe;

        public EndRecipeViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.recipe = state.Ricetta;
            this.HelpMessage = "Bravissimo! Abbiamo finito!";
            this.GreenButtonText = "Esci";
        }

        public void Handle(GUIReadyMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new RecipeMessage(recipe));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            //Navigate to the RecipeStep Screen
            await this.NavigateToScreen<GameSelectionViewModel>();
            base.Handle(message);
        }

        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }
        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }

    }
}