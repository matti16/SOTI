using Caliburn.Micro;
using SOTI.Model;
using System;
using System.Collections.ObjectModel;
using SOTI.Message;

namespace SOTI.ViewModels.Recipe
{
    public class RecipeStepViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>, IHandle<FoodReadedMessage>
    {
        private Passo passo;
        private readonly StateRicetta state;

        public RecipeStepViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.state = state;
            this.passo = state.PassoCorrente;

            showPasso();
        }

        private void showPasso()
        { 
            if (passo.passoDoppio)
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 1)
                {
                    this.eventAggregator.PublishOnUIThread(new PassoMessage(true, passo.ciboGiusto, passo.ciboSbagliato));
                }
                else
                {
                    this.eventAggregator.PublishOnUIThread(new PassoMessage(true, passo.ciboSbagliato, passo.ciboGiusto));
                }
            }
            else
            {
                this.eventAggregator.PublishOnUIThread(new PassoMessage(false, passo.ciboGiusto, null));
            }
        }

        public ObservableCollection<Cibo> Ingredienti { get; private set; }

        public override async void Handle(FoodReadedMessage message)
        {
            if (message.Food == this.passo.ciboGiusto.id.ToString())
            {
                
                //System.Threading.Thread.Sleep(1000);
                if (this.state.HasNext)
                {
                    this.state.MoveNext();
                    //this.passo = state.PassoCorrente;
                    this.eventAggregator.PublishOnUIThread(new IngredientResultMessage(true));
                    await this.NavigateToScreen<RecipeStepViewModel>();
                }
                else
                {
                    await this.NavigateToScreen<ChooseRecipeViewModel>();
                }
            }
            else
            {
                this.eventAggregator.PublishOnUIThread(new IngredientResultMessage(false));
                //await this.NavigateToScreen<RecipeStepViewModel>();
            }
            base.Handle(message);
        }

        public void Handle(GUIReadyMessage message)
        {
            showPasso();
        }


        public override async void Handle(BlueButtonMessage message)
        {
            //Navigate to the ChooseRecipe Screen
            await this.NavigateToScreen<AllergoloRecipeViewModel>();
            base.Handle(message);
        }
    }
}
