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

namespace SOTI.Views.Market
{
    /// <summary>
    /// Logica di interazione per WaitingCardView.xaml
    /// </summary>
    public partial class WaitingCardView : UserControl
    {
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;

        private string audioUri = AudioUri.Audio + AudioUri.Market;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        public WaitingCardView()
        {
            InitializeComponent();

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_Card, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;

            //Play
            CenterMedia.Play();

            //Audio
            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingCard, UriKind.Relative));
            audioPlayer.Play();
        }


        /// <summary>
        /// Sart the timer when the view is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            audioPlayer.Stop();
        }

        /// <summary>
        /// Restart the audio when the timer is over.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingCard, UriKind.Relative));
            audioPlayer.Play();
        }


        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }
        
    }
}
