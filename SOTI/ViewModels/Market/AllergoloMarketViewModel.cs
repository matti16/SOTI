using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;

namespace SOTI.ViewModels.Market
{
    public class AllergoloMarketViewModel : BaseGameScreenViewModel
    {
        private readonly DataLayer data;
        private StateMarket state;

        public AllergoloMarketViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            this.data = data;
            this.state = state;
        }

        public override async void Handle(GreenButtonMessage message)
        {
            await this.NavigateToScreen<WaitingProductsViewModel>();
            base.Handle(message);
        }

    }
}