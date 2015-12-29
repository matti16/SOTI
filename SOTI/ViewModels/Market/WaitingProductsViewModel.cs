﻿using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;

namespace SOTI.ViewModels.Market
{
    public class WaitingProductsViewModel : BaseGameScreenViewModel, IHandle<FoodReadedMessage>
    {
        private readonly DataLayer data;
        private StateMarket state;
        private bool waiting_confirmation = false;
        private const string CARD = "0";
        private const string FRUTTA = "Frutta a Guscio";
        private const string LATTE = "Latte";
        private const string PESCE = "Pesce";
        private const string UOVA = "Uovo";

        public WaitingProductsViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;
        }


        public override async void Handle(FoodReadedMessage message)
        {
            if (message.Food == CARD)
            {
                await this.NavigateToScreen<PaymentViewModel>();
            }
            else
            {
                int id = Int32.Parse(message.Food);
                Cibo readed_food = data.cibi.Find(x => x.id == id);
                state.ReadedFood(readed_food);

                this.eventAggregator.PublishOnUIThread(new FoodInCashMessage(readed_food));
                base.Handle(message);
            }

        }

        public override async void Handle(GreenButtonMessage message)
        {
            if (waiting_confirmation)
            {
                if ((state.Readed_food.frutta && state.Allergia_1.nome == FRUTTA) ||
                     (state.Readed_food.latte && state.Allergia_1.nome == LATTE) ||
                     (state.Readed_food.pesce && state.Allergia_1.nome == PESCE) ||
                     (state.Readed_food.uovo && state.Allergia_1.nome == UOVA))
                {
                    await NavigateToScreen<AllergoloMarketViewModel>();
                }
                else
                {
                    this.eventAggregator.PublishOnUIThread(new FoodConfirmedMessage(true));
                    state.AddToList(state.Readed_food);
                }

            }
            base.Handle(message);
        }

        public override void Handle(RedButtonMessage message)
        {
            if (waiting_confirmation)
            {
                this.eventAggregator.PublishOnUIThread(new FoodConfirmedMessage(false));
            }
            base.Handle(message);
        }


    }
}