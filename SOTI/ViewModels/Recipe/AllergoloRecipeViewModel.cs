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
        private readonly Speech speech;

        public AllergoloRecipeViewModel(IEventAggregator eventAggregator, StateRicetta state, Speech speech) : base(eventAggregator)
        {
            this.HelpMessage = "Ascolta il Dr. Allergolo.";
            this.GreenButtonText = "Continua";
            this.state = state;
            this.speech = speech;
        }

        public void Handle(GUIReadyMessage message)
        {
            bool doble = state.PassoCorrente.passoDoppio;
            if (doble)
            {
                Allergia allergia = state.Allergia;
                this.speech.Say(string.Format("Ciao! Eccovi alcune informazioni. {0}", state.Allergia.descrizione));
                this.eventAggregator.PublishOnUIThread(new AllergoloAllergiaMessage(allergia));
            }
            else
            {
                Cibo ingredient = state.PassoCorrente.ciboGiusto;
                this.speech.Say(string.Format("Ciao! Eccovi alcune informazioni sull'ingrediente {0}. {1}", state.PassoCorrente.ciboGiusto.nome, state.PassoCorrente.ciboGiusto.descrizione));
                this.eventAggregator.PublishOnUIThread(new AllergoloCiboMessage(ingredient));
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            this.speech.Stop();
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<RecipeStepViewModel>();
            base.Handle(message);
        }


        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }
        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }

    }
}