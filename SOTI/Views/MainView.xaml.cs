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
using System.Windows.Threading;

namespace SOTI.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public MainView()
        {
            InitializeComponent();

            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
            timer.Tick += Timer_Tick;
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Cibo.Focus();
        }
    }
}
