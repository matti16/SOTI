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
using System.Windows.Threading;

namespace SOTI.Views.Market
{
    /// <summary>
    /// Logica di interazione per IntroView.xaml
    /// </summary>
    public partial class IntroView : UserControl, IHandle<AllergieMarketMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;

        private string audioUri = AudioUri.Audio + AudioUri.Market;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        public IntroView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Appear, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_AppearEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

            //Audio
            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.Intro, UriKind.Relative));
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
            audioPlayer.Open(new Uri(audioUri + AudioUri.Intro, UriKind.Relative));
            audioPlayer.Play();
        }

        private void CenterMedia_AppearEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Intro, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_AppearEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }

        /// <summary>
        /// Show the two allergies on the screen.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(AllergieMarketMessage message)
        {
            Allergia_1.Text = message.allergia_1.nome.ToUpper();
            Allergia_2.Text = message.allergia_2.nome.ToUpper();

            Allergia_1_img.Source = new BitmapImage(new Uri(allergieUri + message.allergia_1.immagine));
            Allergia_2_img.Source = new BitmapImage(new Uri(allergieUri + message.allergia_2.immagine));
        }
    }
}
