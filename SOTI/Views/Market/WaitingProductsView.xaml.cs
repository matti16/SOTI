﻿using Caliburn.Micro;
using SOTI.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SOTI.Views.Market
{
    /// <summary>
    /// Logica di interazione per WaitingProductsView.xaml
    /// </summary>
    public partial class WaitingProductsView : UserControl, IHandle<FoodInCashMessage>, IHandle<FoodConfirmedMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public WaitingProductsView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            

        }

        public void Handle(FoodConfirmedMessage message)
        {
            if (message.confirmed)
            {
                Product.Text = "Confirmed.";
                Description.Text = "";
            }
            else
            {
                Product.Text = "Thrown Away!";
                Description.Text = "";
            }
        }

        public void Handle(FoodInCashMessage message)
        {
            Product.Text = message.food.nome;
            Description.Text = message.food.descrizione;
        }
    }
}
