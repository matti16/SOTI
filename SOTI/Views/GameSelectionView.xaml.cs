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

namespace SOTI.Views
{
    /// <summary>
    /// Interaction logic for GameSelectionView.xaml
    /// </summary>
    public partial class GameSelectionView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        private int videoState_L = 0;
        private int videoState_C = 0;
        private int videoState_R = 0;

        public GameSelectionView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);


            //Initialize Video Sources
            this.LeftMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Appear, UriKind.Relative);
            this.LeftBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

            this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Appear, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

            this.RightMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Appear, UriKind.Relative);
            this.RightBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

            //Attach event handlers
            LeftMedia.MediaEnded += LeftMedia_MediaEnded;
            LeftBackMedia.MediaEnded += LeftBackMedia_MediaEnded;

            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterBackMedia.MediaEnded += CenterBackMedia_MediaEnded;

            RightMedia.MediaEnded += RightMedia_MediaEnded;
            RightBackMedia.MediaEnded += RightBackMedia_MediaEnded;


            //Play Videos
            LeftMedia.Play();
            CenterMedia.Play();
            RightMedia.Play();

            LeftBackMedia.Play();
            CenterBackMedia.Play();
            RightBackMedia.Play();    

        }


        #region Left Handlers
        private void LeftBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            LeftBackMedia.Position = TimeSpan.Zero;
            LeftBackMedia.Play();
        }

        private void LeftMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_L == 0)
            {
                this.LeftMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Think, UriKind.Relative);
                LeftMedia.Play();
                this.videoState_L++;
            }
            else
            {
                LeftBackMedia.Stop();
                System.Threading.Thread.Sleep(3000);
                LeftMedia.Position = TimeSpan.Zero;
                LeftMedia.Play();
            }
        }
        #endregion

        #region Center Handlers
        private void CenterBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterBackMedia.Position = TimeSpan.Zero;
            CenterBackMedia.Play();
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_C == 0)
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Think, UriKind.Relative);
                CenterMedia.Play();
                this.videoState_C++;
            }
            else
            {
                CenterBackMedia.Stop();
                System.Threading.Thread.Sleep(3000);
                CenterMedia.Position = TimeSpan.Zero;
                CenterMedia.Play();
            }
        }
        #endregion

        #region Right Handlers
        private void RightBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            RightBackMedia.Position = TimeSpan.Zero;
            RightBackMedia.Play();
        }

        private void RightMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_R == 0)
            {
                this.RightMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Think, UriKind.Relative);
                RightMedia.Play();
                this.videoState_R++;
            }
            else
            {
                RightBackMedia.Stop();
                System.Threading.Thread.Sleep(3000);
                RightMedia.Position = TimeSpan.Zero;
                RightMedia.Play();
            }
        }
        
        #endregion


    }
}
