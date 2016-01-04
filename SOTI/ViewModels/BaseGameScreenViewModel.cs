﻿using Caliburn.Micro;
using SOTI.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SOTI.ViewModels
{
    /// <summary>
    /// Classe base da cui tutti i ViewModel che gestiranno le schermate devono ereditare
    /// </summary>
    public class BaseGameScreenViewModel : Screen, IHandle<RedButtonMessage>, IHandle<GreenButtonMessage>, IHandle<BlueButtonMessage>, IHandle<FoodReadedMessage>
    {
        protected readonly IEventAggregator eventAggregator;
        private readonly SerialCommunication serialCommunication;

        //public BaseGameScreenViewModel(IEventAggregator eventAggregator, SerialCommunication serialCommunication)
        //{
        //    this.eventAggregator = eventAggregator;
        //    this.serialCommunication = serialCommunication;
        //}

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

        private bool isNavigating = false;
        public bool IsNavigating
        {
            get { return isNavigating; }
            set
            {
                if (isNavigating != value)
                {
                    isNavigating = value;
                    NotifyOfPropertyChange<bool>(() => IsNavigating);
                }
            }
        }

        private string helpMessage = "HelpMessage";
        public virtual string HelpMessage
        {
            get { return helpMessage; }
            set
            {
                if (helpMessage != value)
                {
                    helpMessage = value;
                    NotifyOfPropertyChange<string>(() => HelpMessage);
                }
            }
        }


        /// <summary>
        /// Ogni volta che un ViewModel viene caricato a schermo, questo metodo viene invocato
        /// </summary>
        protected override void OnActivate()
        {
            this.eventAggregator.Subscribe(this);
            this.IsNavigating = true;
            base.OnActivate();
        }

        protected override void OnViewLoaded(object view)
        {
            this.IsNavigating = false;
            base.OnViewLoaded(view);
        }

        /// <summary>
        /// Ogni volta che un ViewModel viene tolto da schermo, questo metodo viene invocato
        /// </summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            this.eventAggregator.Unsubscribe(this);
            this.IsNavigating = false;
            base.OnDeactivate(close);
        }

        /// <summary>
        /// Metodo da utilizzare per la navigazione da una schermata di gioco all'altra
        /// </summary>
        /// <typeparam name="T">Schermata di destinazione</typeparam>
        public async Task NavigateToScreen<T>() where T : BaseGameScreenViewModel
        {
            this.IsNavigating = true;
            await Task.Delay(1500).ContinueWith(t => this.eventAggregator.PublishOnBackgroundThread((new NavigationMessage(AppBootstrapper.container.GetInstance<T>()))));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Handle(FoodReadedMessage message) { }
    }
}
