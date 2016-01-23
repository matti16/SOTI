using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SOTI.ViewModels.Market
{
    public class IntroViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private StateMarket state;
        private readonly DataLayer data;
        /// <summary>
        /// Costruttore della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public IntroViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.state = state;
            this.data = data;

            this.HelpMessage = "Fai attenzione alle Allergie di Muscolo!";
            this.GreenButtonText = "Conferma";
            this.RedButtonText = "Cambia";

            RandomizeAllergie();
        }

        private void RandomizeAllergie()
        {
            Random rnd = new Random();
            List<Allergia> allergie = data.allergie;
            int first = rnd.Next(allergie.Count);
            int second = rnd.Next(allergie.Count);
            while (second.Equals(first)) { second = rnd.Next(allergie.Count); }

            Allergia firstAllergia = allergie[first];
            Allergia secondAllergia = allergie[second];
            state.InitMarket(firstAllergia, secondAllergia);
        }

        public void Handle(GUIReadyMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new AllergieMarketMessage(state.Allergia_1, state.Allergia_2));
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<WaitingCardViewModel>();
            base.Handle(message);
        }

        public override void Handle(RedButtonMessage message)
        {
            RandomizeAllergie();
            this.eventAggregator.PublishOnUIThread(new AllergieMarketMessage(state.Allergia_1, state.Allergia_2));
            base.Handle(message);
        }

        public override Visibility BlueButtonVisibility { get { return Visibility.Hidden; } }
    }
}
