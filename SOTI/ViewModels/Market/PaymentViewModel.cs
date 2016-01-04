using System;
using Caliburn.Micro;
using SOTI.Model;
using SOTI.Message;
using System.Collections.Generic;

namespace SOTI.ViewModels.Market
{
    public class PaymentViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;
        private List<Purchase> list;
        private int tot = 0;

        public PaymentViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;

            this.HelpMessage = "Bravissimo! Ecco il tuo scontrino! Premi Verde per uscire.";

            list = new List<Purchase>();
            foreach( var pair in state.List_of_Products)
            {
                string product = pair.Key.nome;
                int quantity = pair.Value;
                int price = data.cibi.Find(x => x.id.Equals(pair.Key.id)).prezzo * quantity;
                Purchase item = new Purchase(product, quantity, price);
                this.list.Add(item);
                tot += price;
            }


        }

        public void Handle(GUIReadyMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new ScontrinoMessage(list, tot));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<GameSelectionViewModel>();
            base.Handle(message);
        }
    }
}