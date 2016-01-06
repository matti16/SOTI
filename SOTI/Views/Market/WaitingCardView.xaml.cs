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
    /// Logica di interazione per WaitingCardView.xaml
    /// </summary>
    public partial class WaitingCardView : UserControl
    {
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;

        public WaitingCardView()
        {
            InitializeComponent();

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Appear, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_AppearEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();
        }

        private void CenterMedia_AppearEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_Card, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_AppearEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }
        
    }
}
