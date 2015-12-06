using Caliburn.Micro;
using SOTI.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    public class GameSelectionViewModel : Screen, IHandle<RedButtonMessage>, IHandle<GreenButtonMessage>, IHandle<BlueButtonMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public GameSelectionViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void OnActivate()
        {
            this.eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            this.eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public void Handle(BlueButtonMessage message)
        {
            this.eventAggregator.PublishOnUIThread(new NavigationMessage());
        }

        public void Handle(GreenButtonMessage message)
        {
            throw new NotImplementedException();
        }

        public void Handle(RedButtonMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
