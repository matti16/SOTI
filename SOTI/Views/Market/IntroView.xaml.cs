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
        private readonly IEventAggregator eventAggregator;
        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;

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


        public void Handle(AllergieMarketMessage message)
        {
            Allergia_1.Text = message.allergia_1.nome.ToUpper();
            Allergia_2.Text = message.allergia_2.nome.ToUpper();

            Allergia_1_img.Source = new BitmapImage(new Uri(allergieUri + message.allergia_1.immagine));
            Allergia_2_img.Source = new BitmapImage(new Uri(allergieUri + message.allergia_2.immagine));
        }
    }
}
