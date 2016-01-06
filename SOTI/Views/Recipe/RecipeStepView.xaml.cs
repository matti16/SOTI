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

namespace SOTI.Views.Recipe
{
    /// <summary>
    /// Interaction logic for RecipeStepView.xaml
    /// </summary>
    public partial class RecipeStepView : UserControl, IHandle<PassoMessage>,IHandle<IngredientResultMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        private string baseUri = @"pack://application:,,,/SOTI;component/Media/Images/Cibi/";
        private string videoUri = VideoUri.Video + VideoUri.Cuocolo;
        private string audioUri = AudioUri.Audio + AudioUri.Recipe;
        private bool right_ingredient;

        public RecipeStepView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            this.Loaded += RecipeStepView_Loaded;
            this.Unloaded += RecipeStepView_Unloaded;

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_ingredients, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingIngredients, UriKind.Relative));
            audioPlayer.Play();
            
        }

        private void RecipeStepView_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            audioPlayer.Stop();
        }

        private void RecipeStepView_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 20);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Let the user know that we are waiting for the ingredient every X seconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.WaitingIngredients, UriKind.Relative));
            audioPlayer.Play();
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

                UpIngredient_Txt.Text = message.first.nome.ToUpper();
                //UpIngredient_Img.Source = new BitmapImage(new Uri(baseUri + message.first.immagine));

                BotIngredient_Txt.Text = message.second.nome.ToUpper();
                //BotIngredient_Img.Source = new BitmapImage(new Uri(baseUri + message.second.immagine));

                PassoDoppio_Grid.Visibility = Visibility.Visible;

            }
            else
            {
                PassoDoppio_Grid.Visibility = Visibility.Hidden;
                Ingredient_Txt.Text = message.first.nome.ToUpper();
                //Ingredient_Img.Source = new BitmapImage(new Uri(baseUri + message.first.immagine));
                PassoSingolo_Grid.Visibility = Visibility.Visible;               
            }
        }

        /// <summary>
        /// Method to make the GUI reactive to the feedback regarding the correctness of a readed ingredient.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(IngredientResultMessage message)
        {
            timer.Stop();
            this.right_ingredient = message.right;
            if (message.right == false)
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Wrong_ingredient, UriKind.Relative);
                CenterMedia.MediaEnded -= CenterMedia_MediaEnded;
                CenterMedia.MediaEnded += CenterMedia_MediaEndedResult;

                audioPlayer.Open(new Uri(audioUri + AudioUri.WrongIngredient, UriKind.Relative));
            }
            else
            {
                this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Rigth_ingredient, UriKind.Relative);
                CenterMedia.MediaEnded -= CenterMedia_MediaEnded;
                CenterMedia.MediaEnded += CenterMedia_MediaEndedResult;

                audioPlayer.Open(new Uri(audioUri + AudioUri.RightIngredient, UriKind.Relative));
            }

            audioPlayer.Play();

        }

        private void CenterMedia_MediaEndedResult(object sender, RoutedEventArgs e)
        {
            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting_ingredients, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_MediaEndedResult;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
            timer.Start();
        }

    }
}
