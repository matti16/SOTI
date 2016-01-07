using Caliburn.Micro;
using SOTI.Message;
using SOTI.ViewModels;
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
    /// Interaction logic for ChooseRecipeView.xaml
    /// </summary>
    public partial class ChooseRecipeView : UserControl, IHandle<RecipeMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string recipesUri = @"pack://application:,,,/SOTI;component/Media/Images/Recipes/";
        private string videoUri = VideoUri.Video + VideoUri.Cuocolo;
        private string audioUri = AudioUri.Audio + AudioUri.Recipe;

        public ChooseRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            this.Unloaded += ChooseRecipeView_Unloaded;
            this.Loaded += ChooseRecipeView_Loaded;
            
            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Appear, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_AppearEnded;
            
            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();
            this.eventAggregator.PublishOnUIThread(new RedButtonMessage());

            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.Appear, UriKind.Relative));
            audioPlayer.Play();
            audioPlayer.MediaEnded += AudioPlayer_MediaEnded;

        }

        private void ChooseRecipeView_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();
        }

        private void ChooseRecipeView_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            audioPlayer.Stop();
            this.eventAggregator.Unsubscribe(this);
        }


        private void AudioPlayer_MediaEnded(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.ChooseRecipe, UriKind.Relative));
            audioPlayer.Play();
            audioPlayer.MediaEnded -= AudioPlayer_MediaEnded;
        }

        /// <summary>
        /// Every time the timer ends, repeat the registered audio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.Appear, UriKind.Relative));
            audioPlayer.Play();
            audioPlayer.MediaEnded += AudioPlayer_MediaEnded;
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }

        private void CenterMedia_AppearEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Choosing_recipe, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_AppearEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
        }


        /// <summary>
        /// Manage the RecipeMessage showing in the GUI the new recipe.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(RecipeMessage message)
        {
            string allergia_img = message.recipe.allergia.immagine;
            string recipe_img = message.recipe.immagine;
            this.NomeRicetta.Text = message.recipe.nome.ToUpper();
            Ricetta.Source = new BitmapImage(new Uri(recipesUri + recipe_img));
            Allergia.Source = new BitmapImage(new Uri(allergieUri + allergia_img));
        }

        
    }
}
