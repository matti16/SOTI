using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using SOTI.Message;
using SOTI.Model;

namespace SOTI.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly DataLayer dataLayer;

        public MainViewModel(IEventAggregator eventAggregator, DataLayer dataLayer)
        {
            this.eventAggregator = eventAggregator;
            this.dataLayer = dataLayer;
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

        public void GreenButton()
        {
            this.eventAggregator.PublishOnUIThread(new GreenButtonMessage());
        }

        public void RedButton()
        {
            this.eventAggregator.PublishOnUIThread(new RedButtonMessage());
        }

        public void BlueButton()
        {
            this.eventAggregator.PublishOnUIThread(new BlueButtonMessage());
        }

        public void Handle(NavigationMessage message)
        {
            //ActivateItem(AppBootstrapper.container.BuildUp());
        }
    }
}