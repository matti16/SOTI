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
    /// Logica di interazione per PaymentView.xaml
    /// </summary>
    public partial class PaymentView : UserControl, IHandle<ScontrinoMessage>
    {
        private IEventAggregator eventAggregator;

        public PaymentView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
        }

        public void Handle(ScontrinoMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
