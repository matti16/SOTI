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

namespace SOTI.Views.Recipe
{
    /// <summary>
    /// Interaction logic for ChooseRecipeView.xaml
    /// </summary>
    public partial class ChooseRecipeView : UserControl, IHandle<RecipeMessage>
    {
        private readonly IEventAggregator eventAggregator;
        
        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string recipesUri = @"pack://application:,,,/SOTI;component/Media/Images/Recipes/";
        private string videoUri = VideoUri.Video + VideoUri.Cuocolo;

        public ChooseRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            
            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Choosing_recipe, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterBackMedia.MediaEnded += CenterBackMedia_MediaEnded;
            
            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();
            this.eventAggregator.PublishOnUIThread(new RedButtonMessage());
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }

        private void CenterBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterBackMedia.Position = TimeSpan.Zero;
            CenterBackMedia.Play();
        }
        

        public void Handle(RecipeMessage message)
        {
            string allergia_img = message.recipe.allergia.immagine;
            string recipe_img = message.recipe.immagine;
            Ricetta.Source = new BitmapImage(new Uri(recipesUri + recipe_img));
            Allergia.Source = new BitmapImage(new Uri(allergieUri + allergia_img));
        }
    }
}
