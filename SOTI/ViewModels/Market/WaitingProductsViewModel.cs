using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SOTI.ViewModels.Market
{
    public class WaitingProductsViewModel : BaseGameScreenViewModel, IHandle<FoodReadedMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;
        private bool waiting_confirmation = false;
        private const int CARD = 0;
        private const string FRUTTA = "Frutta a Guscio";
        private const string LATTE = "Latticini";
        private const string PESCE = "Pesce";
        private const string UOVA = "Uovo";

        public WaitingProductsViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;

            this.HelpMessage = "Ora passa sulla cassa i prodotti. Quando hai finito, passa la tessera per pagare.";
            this.GreenButtonText = "Conferma";
            this.RedButtonText = "Scarta";
        }


        public override async void Handle(FoodReadedMessage message)
        {
            if (waiting_confirmation) {  return; }
            if (Int32.Parse(message.Food) == CARD)
            {
                await this.NavigateToScreen<PaymentViewModel>();
            }
            else
            {
                this.HelpMessage = "Vuoi confermare questo prodotto?";
                int id = Int32.Parse(message.Food);
                Cibo readed_food = data.cibi.Find(x => x.id == id);
                state.ReadedFood(readed_food);
                waiting_confirmation = true;
                this.eventAggregator.PublishOnUIThread(new FoodInCashMessage(readed_food));
                base.Handle(message);
            }

        }

        public override async void Handle(GreenButtonMessage message)
        {
            if (waiting_confirmation)
            {
                if ((state.Readed_food.frutta && (state.Allergia_1.nome == FRUTTA || state.Allergia_2.nome == FRUTTA) ) ||
                     (state.Readed_food.latte && (state.Allergia_1.nome == LATTE || state.Allergia_2.nome == LATTE) ) ||
                     (state.Readed_food.pesce && (state.Allergia_1.nome == PESCE || state.Allergia_2.nome == PESCE) ) ||
                     (state.Readed_food.uovo && (state.Allergia_1.nome == UOVA || state.Allergia_2.nome == UOVA) ) )
                {
                    state.AddScarto(state.Readed_food);
                    await NavigateToScreen<AllergoloMarketViewModel>();
                }
                else
                {
                    this.eventAggregator.PublishOnUIThread(new FoodConfirmedMessage(true));
                    this.HelpMessage = "Ora passa sulla cassa i prodotti. Quando hai finito, passa la tessera per pagare.";
                    state.AddToList(state.Readed_food);
                }
                waiting_confirmation = false;
            }
            base.Handle(message);
        }
        

        public override void Handle(RedButtonMessage message)
        {
            if (waiting_confirmation)
            {
                this.eventAggregator.PublishOnUIThread(new FoodConfirmedMessage(false));
                waiting_confirmation = false;
            }
            base.Handle(message);
        }

        private void ShowButtons()
        {
            RedButtonVisibility = Visibility.Visible;
            GreenButtonVisibility = Visibility.Visible;
        }

        public override Visibility GreenButtonVisibility { get { return Visibility.Visible; } }
        public override Visibility RedButtonVisibility { get { return Visibility.Visible; } }
        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }

    }
}