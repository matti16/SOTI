using Caliburn.Micro;
using SOTI.Model;
using System;
using System.Collections.ObjectModel;
using SOTI.Message;

namespace SOTI.ViewModels.Recipe
{
    public class RecipeStepViewModel : BaseGameScreenViewModel
    {
        private readonly Passo passo;
        private readonly StateRicetta state;

        public RecipeStepViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.state = state;
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
                this.Giusto = passo.ciboGiusto.nome;
                this.Sbagliato = passo.ciboSbagliato.nome;
            }
            else
            {
                this.Ingredienti.Add(passo.ciboGiusto);
                this.Giusto = passo.ciboGiusto.nome;
            }
        }

        public ObservableCollection<Cibo> Ingredienti { get; private set; }

        public override void Handle(FoodReadedMessage message)
        {
            if (message.Food == this.passo.ciboGiusto.id.ToString())
            {
                this.Risultato = "Bravo";
                System.Threading.Thread.Sleep(1000);
                if (this.state.HasNext)
                {
                    this.state.MoveNext();
                    this.NavigateToScreen<RecipeStepViewModel>();
                }
                else
                {
                    this.NavigateToScreen<ChooseRecipeViewModel>();
                }
            }
            else
            {
                this.Risultato = "NOOOO";
            }
            base.Handle(message);
        }

        private string giusto = "";
        public virtual string Giusto
        {
            get { return giusto; }
            set
            {
                if (giusto != value)
                {
                    giusto = value;
                    NotifyOfPropertyChange<string>(() => Giusto);
                }
            }
        }

        private string sbagliato = "";
        public virtual string Sbagliato
        {
            get { return sbagliato; }
            set
            {
                if (sbagliato != value)
                {
                    sbagliato = value;
                    NotifyOfPropertyChange<string>(() => Sbagliato);
                }
            }
        }

        private string risultato = "";
        public virtual string Risultato
        {
            get { return risultato; }
            set
            {
                if (risultato != value)
                {
                    risultato = value;
                    NotifyOfPropertyChange<string>(() => Risultato);
                }
            }
        }

        
    }
}
