using System;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System.Windows;

namespace SOTI.ViewModels.Market
{
    public class AllergoloMarketViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;
        private readonly Speech speech;

        public AllergoloMarketViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state, Speech speech) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;
            this.speech = speech;

            this.HelpMessage = "Riporta indietro il prodotto! Muscolo è allergico";
            this.GreenButtonText = "Continua";
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            this.speech.Stop();
        }

        public void Handle(GUIReadyMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new WrongProductMessage(state.Readed_food, state.Allergia_1, state.Allergia_2));
            this.speech.Say(string.Format("Attenzione! Non puoi comprare {0}. Ricordati che muscolo è allergico a {1} e {2}.",
                state.Readed_food.nome, state.Allergia_1.nome, state.Allergia_2.nome));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<WaitingProductsViewModel>();
            base.Handle(message);
        }

        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }
        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }

    }
}