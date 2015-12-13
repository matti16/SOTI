using System.Windows;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;

namespace SOTI.ViewModels.Recipe
{
    public class ChooseRecipeViewModel : BaseGameScreenViewModel
    {
        private DataLayer data;
        private int currentRecipe;
        /// <summary>
        /// Costrutto della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public ChooseRecipeViewModel(IEventAggregator eventAggregator, DataLayer data) : base(eventAggregator)
        {
            this.data = data;
            Random rnd = new Random();
            int firstRecipe = rnd.Next(data.ricette.Count - 1);
            currentRecipe = firstRecipe;
            showRecipe(data.ricette[currentRecipe]);
            
        }

        /// <summary>
        /// Method that should modify the interface in order to change the shown Recipe.
        /// </summary>
        /// <param name="ricetta"></param>
        private void showRecipe(Ricetta ricetta)
        {
            ;
        }

        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }

        public override void Handle(GreenButtonMessage message)
        {
            //Navigate to the RecipeStep Screen
            this.NavigateToScreen<RecipeStepViewModel>();
            base.Handle(message);
        }

        public override void Handle(RedButtonMessage message)
        {
            currentRecipe++;
            if (currentRecipe >= data.ricette.Count)
            {
                currentRecipe = 0;
            }
            showRecipe(data.ricette[currentRecipe]);
            base.Handle(message);
        }

    }
}
