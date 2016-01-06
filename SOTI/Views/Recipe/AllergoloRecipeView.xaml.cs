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
    /// Logica di interazione per AllergoloRecipeView.xaml
    /// </summary>
    public partial class AllergoloRecipeView : UserControl, IHandle<AllergoloAllergiaMessage>, IHandle<AllergoloCiboMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private string allergieUri = @"pack://application:,,,/SOTI;component/Media/Images/Allergie/";
        private string cibiUri = @"pack://application:,,,/SOTI;component/Media/Images/Cibi/";

        private string videoUri = VideoUri.Video + VideoUri.Allergolo;

        private string audioUri = AudioUri.Audio + AudioUri.Recipe;
        private MediaPlayer audioPlayer;

        public AllergoloRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            //Load Video
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Appear_Recipe, UriKind.Relative);
            this.CenterBackMedia.Source = new Uri(videoUri + VideoUri.Blink, UriKind.Relative);

            //Handlers
            CenterMedia.MediaEnded += CenterMedia_AppearEnded;

            //Play
            CenterMedia.Play();
            CenterBackMedia.Play();

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());

            audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(audioUri + AudioUri.AllergoloRecipe, UriKind.Relative));
            audioPlayer.Play();
            this.Unloaded += View_Unloaded;
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            audioPlayer.Stop();
        }

        private void CenterMedia_AppearEnded(object sender, RoutedEventArgs e)
        {
            this.CenterMedia.Source = new Uri(videoUri + VideoUri.Waiting, UriKind.Relative);
            CenterMedia.MediaEnded -= CenterMedia_AppearEnded;
            CenterMedia.MediaEnded += CenterMedia_MediaEnded;
            CenterMedia.Play();
        }

        private void CenterMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            CenterMedia.Position = TimeSpan.Zero;
            CenterMedia.Play();
        }
        

        public void Handle(AllergoloCiboMessage message)
        {
            Title.Text = message.ingredient.nome.ToUpper();
            Description.Text = message.ingredient.descrizione;
            Immagine.Source = new BitmapImage(new Uri(cibiUri + message.ingredient.immagine));
        }

        public void Handle(AllergoloAllergiaMessage message)
        {
            Title.Text = message.allergia.nome.ToUpper();
            Description.Text = message.allergia.descrizione;
            Immagine.Source = new BitmapImage(new Uri(allergieUri + message.allergia.immagine));
        }


    }
}
