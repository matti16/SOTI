using System;
using Caliburn.Micro;
using SOTI.Model;
using SOTI.Message;
using System.Collections.Generic;
using System.Windows;

namespace SOTI.ViewModels.Market
{
    public class PaymentViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;
        private List<Purchase> list;
        private List<Cibo> scarti;
        private int tot = 0;

        public PaymentViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;

            this.HelpMessage = "Bravissimo! Leggi sopra quali prodotti non hai potuto prendere!";
            this.GreenButtonText = "Esci";

            this.scarti = state.Scartati;
            /* list = new List<Purchase>();
            foreach( var pair in state.List_of_Products)
            {
                string product = pair.Key.nome;
                int quantity = pair.Value;
                int price = data.cibi.Find(x => x.id.Equals(pair.Key.id)).prezzo * quantity;
                Purchase item = new Purchase(product, quantity, price);
                this.list.Add(item);
                tot += price;
            } */


        }

        public void Handle(GUIReadyMessage message)
        {
            //this.eventAggregator.PublishOnUIThread(new ScontrinoMessage(list, tot));
            this.eventAggregator.PublishOnUIThread(new ScartiMessage(scarti));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<GameSelectionViewModel>();
            base.Handle(message);
        }

        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }
        public override Visibility RedButtonVisibility { get { return Visibility.Hidden; } }
    }
}