using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels.Market
{
    class IntroViewModel : BaseGameScreenViewModel, IHandle<GUIReadyMessage>
    {
        private StateMarket state;
        /// <summary>
        /// Costruttore della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public IntroViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.state = state;
            Random rnd = new Random();
            List<Allergia> allergie = data.allergie;
            int first = rnd.Next(allergie.Count);
            Allergia firstAllergia = allergie[first];

            allergie.RemoveAt(first);
            int second = rnd.Next(allergie.Count);
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
    }
}
