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
using System.Windows.Media;
using SOTI.ViewModels.Market;

namespace SOTI.ViewModels
{
    /// <summary>
    /// It manages the life cycle of the application.
    /// </summary>
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<NavigationMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly SerialCommunication serialCommunication;
        private readonly DataLayer data;
        private MediaPlayer audioPlayer;
        
        
        public MainViewModel(IEventAggregator eventAggregator, GameSelectionViewModel gameSelectionViewModel, SerialCommunication serialCommunication, DataLayer data)
        {
            this.eventAggregator = eventAggregator;
            this.serialCommunication = serialCommunication;
            this.data = data;
            audioPlayer = new MediaPlayer();
            ActivateItem(gameSelectionViewModel);
        }

        /*
        public MainViewModel(IEventAggregator eventAggregator, GameSelectionViewModel gameSelectionViewModel, DataLayer data)
        {
            this.eventAggregator = eventAggregator;
            this.data = data;
            audioPlayer = new MediaPlayer();
            ActivateItem(gameSelectionViewModel);
        }*/

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
                Tags t = data.tags.Find(x => x.tag.Equals(this.Cibo));
                this.Cibo = "";
                if (t == null)
                    return;

                string idCibo = t.id;
                this.eventAggregator.PublishOnUIThread(new FoodReadedMessage(idCibo));              
            }
        }

        /// <summary>
        /// Metodo per la gestione del bottone Verde
        /// </summary>
        public void GreenButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            audioPlayer.Open(new Uri("Media/Audio/button.mp3", UriKind.Relative));
            this.audioPlayer.Play();
            this.eventAggregator.PublishOnUIThread(new GreenButtonMessage());
        }

        /// <summary>
        /// Metodo per la gestione del bottone Rosso
        /// </summary>
        public void RedButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            audioPlayer.Open(new Uri("Media/Audio/button.mp3", UriKind.Relative));
            this.audioPlayer.Play();
            this.eventAggregator.PublishOnUIThread(new RedButtonMessage());
        }

        /// <summary>
        /// Metodo per la gestione del bottone Blu
        /// </summary>
        public void BlueButton()
        {
            // Gestione di Arduino
            // Informo tutti gli interessati che il bottone è stato premuto
            audioPlayer.Open(new Uri("Media/Audio/button.mp3", UriKind.Relative));
            this.audioPlayer.Play();
            this.eventAggregator.PublishOnUIThread(new BlueButtonMessage());
        }

        public void Handle(NavigationMessage message)
        {
            ActivateItem(message.DestinationScreen);
        }
    }
}
