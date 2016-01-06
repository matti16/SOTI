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
    /// Logica di interazione per AllergoloMarketView.xaml
    /// </summary>
    public partial class AllergoloMarketView : UserControl, IHandle<WrongProductMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string videoUri = VideoUri.Video + VideoUri.Allergolo;

        private string audioUri = AudioUri.Audio + AudioUri.Market;
        private MediaPlayer audioPlayer;

        public AllergoloMarketView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Appear_Market, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_AppearEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.AllergoloMarket, UriKind.Relative));
            audioPlayer.Play();
        }

        private void CenterMedia_AppearEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_AppearEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
        }

        public void Handle(WrongProductMessage message)
        {
            Product.Text = message.product.nome.ToUpper() + ".";
            Allergie.Text = message.allergia_1.nome.ToUpper() + " e " + message.allergia_2.nome.ToUpper() + ".";
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }
        
        
    }
}
