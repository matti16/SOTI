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
    /// Logica di interazione per WaitingProductsView.xaml
    /// </summary>
    public partial class WaitingProductsView : UserControl, IHandle<FoodInCashMessage>, IHandle<FoodConfirmedMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;
        private string cibiUri = @"pack://application:,,,/SOTI;component/Media/Images/Cibi/";

        private string audioUri = AudioUri.Audio + AudioUri.Market;
        private MediaPlayer audioPlayer = new MediaPlayer();
        private DispatcherTimer timer = new DispatcherTimer();

        public WaitingProductsView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            ProductGrid.Visibility = Visibility.Hidden;

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_Products, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();
            //Audio
            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
            timer.Tick += Timer_Tick;
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingProducts, UriKind.Relative));
            audioPlayer.Play();
        }


        /// <summary>
        /// Sart the timer when the view is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Loaded(object sender, RoutedEventArgs e)
        {
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
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingProducts, UriKind.Relative));
            audioPlayer.Play();
        }



        public void Handle(FoodConfirmedMessage message)
        {
            audioPlayer.Stop();
            if (message.confirmed)
            {
                this.CenterMedia.Source = new Uri(videoUri + VideoUri.Product_OK, UriKind.Relative);
                CenterMedia.MediaEnded -= CenterMedia_MediaEnded;
                CenterMedia.MediaEnded += CenterMedia_ConfirmEnded;
                CenterMedia.Play();      
            }
            else
            {
                ProductGrid.Visibility = Visibility.Hidden;
                CashGrid.Visibility = Visibility.Visible;
            }
            timer.Start();
        }

        /// <summary>
        /// Event Handler that is added to the Media Element to know when the video of confirmation is ended.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CenterMedia_ConfirmEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_Products, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_ConfirmEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
            ProductGrid.Visibility = Visibility.Hidden;
            CashGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// When a food is readed, the related information are shown and the video is changed.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(FoodInCashMessage message)
        {
            timer.Stop();
            ProductName.Text = message.food.nome.ToUpper();
            ProductDescription.Text = message.food.descrizione;
            ProductImg.Source = new BitmapImage(new Uri(cibiUri + message.food.immagine));

            CashGrid.Visibility = Visibility.Hidden;
            ProductGrid.Visibility = Visibility.Visible;

            audioPlayer.Open(new Uri(audioUri + AudioUri.ConfirmProduct, UriKind.Relative));
            audioPlayer.Play();
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_Confirm, UriKind.Relative);
            CenterMedia.Play();
        }


        /// <summary>
        /// Restart the videos when ends.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }
        
    }
}
