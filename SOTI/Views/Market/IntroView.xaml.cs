using Caliburn.Micro;
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
    /// Logica di interazione per IntroView.xaml
    /// </summary>
    public partial class IntroView : UserControl, IHandle<AllergieMarketMessage>
    {
        private IEventAggregator eventAggregator;

        public IntroView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            
            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

        }

        public void Handle(AllergieMarketMessage message)
        {
            Allergia_1.Text = message.allergia_1.nome;
            Allergia_2.Text = message.allergia_2.nome;
        }
    }
}
