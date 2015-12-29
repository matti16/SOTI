using System;
using Caliburn.Micro;
using SOTI.Message;

namespace SOTI.ViewModels.Market
{
    public class WaitingCardViewModel : BaseGameScreenViewModel, IHandle<FoodReadedMessage>
    {
        private const string CARD = "0";

        public WaitingCardViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public override async void Handle(FoodReadedMessage message)
        {
            if (message.Food == CARD)
            {
                await NavigateToScreen<WaitingProductsViewModel>();
            }
            base.Handle(message);
        }

    }
}