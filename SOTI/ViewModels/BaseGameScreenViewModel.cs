using Caliburn.Micro;
using SOTI.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SOTI.ViewModels
{
    /// <summary>
    /// Classe base da cui tutti i ViewModel che gestiranno le schermate devono ereditare
    /// </summary>
    public class BaseGameScreenViewModel : Screen, IHandle<RedButtonMessage>, IHandle<GreenButtonMessage>, IHandle<BlueButtonMessage>
    {
        protected readonly IEventAggregator eventAggregator;

        public BaseGameScreenViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        #region Green Button Properties

        private Visibility greenButtonVisibility = Visibility.Visible;
        public virtual Visibility GreenButtonVisibility
        {
            get { return greenButtonVisibility; }
            set
            {
                if (greenButtonVisibility != value)
                {
                    greenButtonVisibility = value;
                    NotifyOfPropertyChange<Visibility>(() => GreenButtonVisibility);
                }
            }
        }

        private string greenButtonText = "Green";
        public virtual string GreenButtonText
        {
            get { return greenButtonText; }
            set
            {
                if (greenButtonText != value)
                {
                    greenButtonText = value;
                    NotifyOfPropertyChange<string>(() => GreenButtonText);
                }
            }
        }

        #endregion

        #region Red Button Properties

        private Visibility redButtonVisibility = Visibility.Visible;
        public virtual Visibility RedButtonVisibility
        {
            get { return redButtonVisibility; }
            set
            {
                if (redButtonVisibility != value)
                {
                    redButtonVisibility = value;
                    NotifyOfPropertyChange<Visibility>(() => RedButtonVisibility);
                }
            }
        }

        private string redButtonText = "Red";
        public virtual string RedButtonText
        {
            get { return redButtonText; }
            set
            {
                if (redButtonText != value)
                {
                    redButtonText = value;
                    NotifyOfPropertyChange<string>(() => RedButtonText);
                }
            }
        }

        #endregion

        #region Blue Button Properties

        private Visibility blueButtonVisibility = Visibility.Visible;
        public virtual Visibility BlueButtonVisibility
        {
            get { return blueButtonVisibility; }
            set
            {
                if (blueButtonVisibility != value)
                {
                    blueButtonVisibility = value;
                    NotifyOfPropertyChange<Visibility>(() => BlueButtonVisibility);
                }
            }
        }

        private string blueButtonText = "Blue";
        public virtual string BlueButtonText
        {
            get { return blueButtonText; }
            set
            {
                if (blueButtonText != value)
                {
                    blueButtonText = value;
                    NotifyOfPropertyChange<string>(() => BlueButtonText);
                }
            }
        }

        #endregion

        /// <summary>
        /// Ogni volta che un ViewModel viene caricato a schermo, questo metodo viene invocato
        /// </summary>
        protected override void OnActivate()
        {
            this.eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        /// <summary>
        /// Ogni volta che un ViewModel viene tolto da schermo, questo metodo viene invocato
        /// </summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            this.eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        /// <summary>
        /// Metodo da utilizzare per la navigazione da una schermata di gioco all'altra
        /// </summary>
        /// <typeparam name="T">Schermata di destinazione</typeparam>
        public void NavigateToScreen<T>() where T : BaseGameScreenViewModel
        {
            this.eventAggregator.PublishOnUIThread(new NavigationMessage(AppBootstrapper.container.GetInstance<T>()));
        }

        /// <summary>
        /// Ogni volta che viene premuto il bottone BLU, questo metodo viene invocato
        /// </summary>
        /// <param name="message">Non Utilizzato</param>
        public virtual void Handle(BlueButtonMessage message) { }

        /// <summary>
        /// Ogni volta che viene premuto il bottone VERDE, questo metodo viene invocato
        /// </summary>
        /// <param name="message">Non Utilizzato</param>
        public virtual void Handle(GreenButtonMessage message) { }

        /// <summary>
        /// Ogni volta che viene premuto il bottone ROSSO, questo metodo viene invocato
        /// </summary>
        /// <param name="message">Non Utilizzato</param>
        public virtual void Handle(RedButtonMessage message) { }
    }
}
