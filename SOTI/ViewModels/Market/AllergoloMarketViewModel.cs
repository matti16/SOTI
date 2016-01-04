using System;
using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;

namespace SOTI.ViewModels.Market
{
    public class AllergoloMarketViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;

        public AllergoloMarketViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;

            this.HelpMessage = "Riporta indietro il prodotto. Poi premi Verde per continuare.";
        }

        public void Handle(GUIReadyMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new WrongProductMessage(state.Readed_food, state.Allergia_1, state.Allergia_2));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<WaitingProductsViewModel>();
            base.Handle(message);
        }

    }
}