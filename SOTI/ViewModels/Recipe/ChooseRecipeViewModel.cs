using System.Windows;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;

namespace SOTI.ViewModels.Recipe
{
    public class ChooseRecipeViewModel : BaseGameScreenViewModel
    {
        private readonly DataLayer data;
        private readonly StateRicetta state;
        private int currentRecipe;
        private Ricetta ricetta;

        /// <summary>
        /// Costruttore della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public ChooseRecipeViewModel(IEventAggregator eventAggregator, DataLayer data, StateRicetta state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;

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
            this.ricetta = ricetta;
            this.NomeRicetta = this.ricetta.nome;
            this.Allergia = this.ricetta.allergia.nome;
        }

        private string nomeRicetta = "Ricetta";
        public string NomeRicetta
        {
            get { return nomeRicetta; }
            set
            {
                if (nomeRicetta != value)
                {
                    nomeRicetta = value;
                    NotifyOfPropertyChange<string>(() => NomeRicetta);
                }
            }
        }
        private string allergia = "Allergia";
        public string Allergia
        {
            get { return allergia; }
            set
            {
                if (allergia != value)
                {
                    allergia = value;
                    NotifyOfPropertyChange<string>(() => Allergia);
                }
            }
        }

        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }

        public override void Handle(GreenButtonMessage message)
        {
            //Navigate to the RecipeStep Screen
            this.state.initRicetta(ricetta);
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
