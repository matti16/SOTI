using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
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
    /// Logica di interazione per PaymentView.xaml
    /// </summary>
    public partial class PaymentView : UserControl, IHandle<ScartiMessage> ///,IHandle<ScontrinoMessage> 
    {
        private readonly IEventAggregator eventAggregator;
        private string videoUri = VideoUri.Video + VideoUri.Muscolo;

        private string audioUri = AudioUri.Audio + AudioUri.Market;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        public PaymentView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Scontrino, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;

            //Play
            CenterMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
            //Audio
            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.Scontrino, UriKind.Relative));
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
            this.eventAggregator.Unsubscribe(this);
        }

        /// <summary>
        /// Restart the audio when the timer is over.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.Scontrino, UriKind.Relative));
            audioPlayer.Play();
        }

        /* SCONTRINO NO MORE USED
        /// <summary>
        /// Show the information about the shopping.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ScontrinoMessage message)
        {
            int i = 0; int partial = 0;
            foreach (var item in message.list)
            {
                if (i >= 9)
                {
                    Prodotti.Text += "Altro...\n";
                    //Prezzi.Text += (message.tot - partial).ToString() + " €   \n";
                    break;
                }
                Prodotti.Text += item.product.ToUpper() + " x" + item.quantity.ToString() + "\n";
                Prezzi.Text += item.price.ToString() + " €   \n";
                partial += item.price;
                i++;
            }
            Tot.Text = message.tot.ToString() + " €   \n";
        }
        */


        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }

        /// <summary>
        /// Build the list of thrown products
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ScartiMessage message)
        {
            List<Cibo> scarti = message.scarti;
            foreach (var item in scarti)
            {
                Scarti.Text += item.nome + "\n\n";
                string allergie = "";
                if (item.latte) { allergie += "Latticini"; }
                if (item.uovo) { allergie += " Uova"; }
                if (item.frutta) { allergie += " Frutta a Guscio"; }
                if (item.pesce) { allergie += " Pesce"; }
                allergie += "\n\n";
                Allergie.Text += allergie;

            }
        }
    }
}
