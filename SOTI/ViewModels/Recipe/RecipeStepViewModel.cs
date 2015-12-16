using Caliburn.Micro;
using SOTI.Model;
using System;
using System.Collections.ObjectModel;

namespace SOTI.ViewModels.Recipe
{
    public class RecipeStepViewModel : BaseGameScreenViewModel
    {
        private readonly Passo passo;

        public RecipeStepViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.passo = state.PassoCorrente;
            this.Ingredienti = new ObservableCollection<Cibo>();

            if (passo.passoDoppio)
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 1)
                {
                    //Metto prima quello sbagliato
                    this.Ingredienti.Add(passo.ciboSbagliato);
                    //Poi quello giusto
                    this.Ingredienti.Add(passo.ciboGiusto);
                }
                else
                {
                    //Metto prima quello giusto
                    this.Ingredienti.Add(passo.ciboGiusto);
                    //Poi quello sbagliato
                    this.Ingredienti.Add(passo.ciboSbagliato);
                }
            }
            else
                this.Ingredienti.Add(passo.ciboGiusto);
        }

        public ObservableCollection<Cibo> Ingredienti { get; private set; }
    }
}
