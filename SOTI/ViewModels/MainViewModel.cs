using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using SOTI.Message;
using SOTI.Model;
using SOTI.ViewModels.Recipe;
using System.Windows.Input;

namespace SOTI.ViewModels
{
    /// <summary>
    /// ViewModel utilizzato per la gestione del ciclo di vita dell'applicazione
    /// </summary>
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly SerialCommunication serialCommunication;

        /*
        public MainViewModel(IEventAggregator eventAggregator, GameSelectionViewModel gameSelectionViewModel, SerialCommunication serialCommunication)
        {
            this.eventAggregator = eventAggregator;
            this.serialCommunication = serialCommunication;
            ActivateItem(gameSelectionViewModel);
        }
        */

        public MainViewModel(IEventAggregator eventAggregator, GameSelectionViewModel gameSelectionViewModel)
        {
            this.eventAggregator = eventAggregator;
            ActivateItem(gameSelectionViewModel);
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

        private string cibo = "";
        public virtual string Cibo
        {
            get { return cibo; }
            set
            {
                if (cibo != value)
                {
                    cibo = value;
                    NotifyOfPropertyChange<string>(() => Cibo);
                }
            }
        }

        public void ShowNameAction(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                this.eventAggregator.PublishOnUIThread(new FoodReadedMessage(this.Cibo));
            }
        }

        /// <summary>
        /// Metodo per la gestione del bottone Verde
        /// </summary>
        public void GreenButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            this.eventAggregator.PublishOnUIThread(new GreenButtonMessage());
        }

        /// <summary>
        /// Metodo per la gestione del bottone Rosso
        /// </summary>
        public void RedButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            this.eventAggregator.PublishOnUIThread(new RedButtonMessage());
        }

        /// <summary>
        /// Metodo per la gestione del bottone Blu
        /// </summary>
        public void BlueButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            this.eventAggregator.PublishOnUIThread(new BlueButtonMessage());
        }

        public void Handle(NavigationMessage message)
        {
            ActivateItem(message.DestinationScreen);
        }
    }
}