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
    /// Logica di interazione per EndRecipeView.xaml
    /// </summary>
    public partial class EndRecipeView : UserControl, IHandle<RecipeMessage>
    {
        private IEventAggregator eventAggregator;
        private string recipesUri = @"pack://application:,,,/SOTI;component/Media/Images/Recipes/";
        private string videoUri = VideoUri.Video + VideoUri.Cuocolo;

        public EndRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

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
            this.NomeRicetta.Text = message.recipe.nome;
            this.Ricetta.Source = new BitmapImage(new Uri(recipesUri + message.recipe.immagine));

        }
    }
}
