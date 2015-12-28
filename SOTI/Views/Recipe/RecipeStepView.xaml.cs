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

namespace SOTI.Views.Recipe
{
    /// <summary>
    /// Interaction logic for RecipeStepView.xaml
    /// </summary>
    public partial class RecipeStepView : UserControl, IHandle<PassoMessage>,IHandle<IngredientResultMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string baseUri = @"pack://application:,,,/SOTI;component/Media/Images/Cibi/";
        private bool right_ingredient;

        public RecipeStepView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            resetVideo();
            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
        }

        private void resetVideo()
        {
            //Load Video
            this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Think, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterBackMedia.MediaEnded += CenterBackMedia_MediaEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();
        }

        private void CenterBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterBackMedia.Position = TimeSpan.Zero;
            CenterBackMedia.Play();
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }

        /// <summary>
        /// Method to update the view with the requested ingredients for the recipe.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(PassoMessage message)
        {
            if (message.doble)
            {
                PassoSingolo_Grid.Visibility = Visibility.Hidden;

                UpIngredient_Txt.Text = message.first.nome;
                //UpIngredient_Img.Source = new BitmapImage(new Uri(baseUri + message.first.immagine));

                BotIngredient_Txt.Text = message.second.nome;
                //BotIngredient_Img.Source = new BitmapImage(new Uri(baseUri + message.second.immagine));

                PassoDoppio_Grid.Visibility = Visibility.Visible;

            }
            else
            {
                PassoDoppio_Grid.Visibility = Visibility.Hidden;
                Ingredient_Txt.Text = message.first.nome;
                //Ingredient_Img.Source = new BitmapImage(new Uri(baseUri + message.first.immagine));
                PassoSingolo_Grid.Visibility = Visibility.Visible;               
            }
        }

        public void Handle(IngredientResultMessage message)
        {
            this.right_ingredient = message.right;
            if (message.right == false)
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Wrong_ingredient, UriKind.Relative);
                CenterMedia.MediaEnded -= CenterMedia_MediaEnded;
                CenterMedia.MediaEnded += CenterMedia_MediaEndedResult;
            }
            else
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Rigth_ingredient, UriKind.Relative);
                CenterMedia.MediaEnded -= CenterMedia_MediaEnded;
                CenterMedia.MediaEnded += CenterMedia_MediaEndedResult;
            }
        }

        private void CenterMedia_MediaEndedResult(object sender, RoutedEventArgs e)
        {
            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
            resetVideo();
        }
    }
}
