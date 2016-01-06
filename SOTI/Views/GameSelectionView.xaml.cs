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
            this.LeftMedia.Source = new Uri(VideoUri.Video + VideoUri.Allergolo + VideoUri.Appear_Recipe, UriKind.Relative);
            this.LeftBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Allergolo + VideoUri.Blink, UriKind.Relative);

            this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Muscolo + VideoUri.Appear, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Muscolo + VideoUri.Blink, UriKind.Relative);

            this.RightMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Appear, UriKind.Relative);
            this.RightBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

            //Attach event handlers
            LeftMedia.MediaEnded += LeftMedia_MediaEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            RightMedia.MediaEnded += RightMedia_MediaEnded;

            //Play Videos
            LeftMedia.Play();
            CenterMedia.Play();
            RightMedia.Play();

            LeftBackMedia.Play();
            CenterBackMedia.Play();
            RightBackMedia.Play();    

        }


        #region Left Handlers
        private void LeftMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_L == 0)
            {
                this.LeftMedia.Source = new Uri(VideoUri.Video + VideoUri.Allergolo + VideoUri.Waiting, UriKind.Relative);
                LeftMedia.Play();
                this.videoState_L++;
            }
            else
            {
                LeftMedia.Position = TimeSpan.Zero;
                LeftMedia.Play();
            }
        }
        #endregion

        #region Center Handlers
        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_C == 0)
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Muscolo + VideoUri.Waiting_Products, UriKind.Relative);
                CenterMedia.Play();
                this.videoState_C++;
            }
            else
            {
                CenterMedia.Position = TimeSpan.Zero;
                CenterMedia.Play();
            }
        }
        #endregion

        #region Right Handlers

        private void RightMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.videoState_R == 0)
            {
                this.RightMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Waiting_ingredients, UriKind.Relative);
                RightMedia.Play();
                this.videoState_R++;
            }
            else
            {
                RightMedia.Position = TimeSpan.Zero;
                RightMedia.Play();
            }
        }
        
        #endregion


    }
}
