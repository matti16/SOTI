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
    public partial class ChooseRecipeView : UserControl, IHandle<GreenButtonMessage>, IHandle<RecipeMessage>
    {
        private readonly IEventAggregator eventAggregator;

        private int videoState = 0;
        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string recipesUri = @"pack://application:,,,/SOTI;component/Media/Images/Recipes/";

        public ChooseRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);
            
            //Load Video
            this.CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Relax, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);

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
            switch (videoState)
            {
                case 0:
                    CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Point_Left_Start, UriKind.Relative);
                    CenterMedia.Play();
                    videoState = 1;
                    break;
                case 1:
                    CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Blink, UriKind.Relative);
                    CenterMedia.Play();
                    videoState = 2;
                    break;
                case 2:
                    CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Scratch_start, UriKind.Relative);
                    CenterMedia.Play();
                    videoState = 0;
                    break;
            }

        }

        private void CenterBackMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterBackMedia.Position = TimeSpan.Zero;
            CenterBackMedia.Play();
        }

        public void Handle(GreenButtonMessage message)
        {
            CenterMedia.Source = new Uri(VideoUri.Video + VideoUri.Cuocolo + VideoUri.Thumb_Up, UriKind.Relative);
            CenterMedia.Play();
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
