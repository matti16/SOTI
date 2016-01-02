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
        private bool right = false;

        public RecipeStepViewModel(IEventAggregator eventAggregator, StateRicetta state) : base(eventAggregator)
        {
            this.state = state;
            showPasso();
        }

        private void showPasso()
        {
            this.passo = state.PassoCorrente;
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
        

        public override async void Handle(FoodReadedMessage message)
        {
            if (message.Food == this.passo.ciboGiusto.id.ToString())
            {
                
                //System.Threading.Thread.Sleep(1000);
                if (this.state.HasNext)
                {
                    this.state.MoveNext();
                    right = true;
                    this.eventAggregator.PublishOnUIThread(new IngredientResultMessage(true));
                    
                }
                else
                {
                    await NavigateToScreen<EndRecipeViewModel>();
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
            if (right)
            {
                showPasso();
                //await this.NavigateToScreen<RecipeStepViewModel>();
            }
            else
            {
                showPasso();
            }

        }


        public override async void Handle(BlueButtonMessage message)
        {
            //Navigate to the ChooseRecipe Screen
            await this.NavigateToScreen<AllergoloRecipeViewModel>();
            base.Handle(message);
        }
    }
}
