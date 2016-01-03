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
    /// Logica di interazione per EndRecipeView.xaml
    /// </summary>
    public partial class EndRecipeView : UserControl, IHandle<RecipeMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string recipesUri = @"pack://application:,,,/SOTI;component/Media/Images/Recipes/";
        private string videoUri = VideoUri.Video + VideoUri.Cuocolo;
        private string audioUri = AudioUri.Audio + AudioUri.Recipe;
        private MediaPlayer audioPlayer;
        private DispatcherTimer timer = new DispatcherTimer();

        public EndRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            this.Loaded += EndRecipeView_Loaded;
            this.Unloaded += EndRecipeView_Unloaded;

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Recipe_finish, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterBackMedia.MediaEnded += CenterBackMedia_MediaEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.FinishRecipe, UriKind.Relative));
            audioPlayer.Play();
        }

        private void EndRecipeView_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            audioPlayer.Stop();
        }

        private void EndRecipeView_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 9);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            audioPlayer.Open(new Uri(audioUri + AudioUri.FinishRecipe, UriKind.Relative));
            audioPlayer.Play();
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

        public void Handle(RecipeMessage message)
        {
            this.NomeRicetta.Text = message.recipe.nome.ToUpper();
            this.Ricetta.Source = new BitmapImage(new Uri(recipesUri + message.recipe.immagine));

        }
    }
}
