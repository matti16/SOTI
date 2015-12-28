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

        public AllergoloRecipeView()
        {
            InitializeComponent();
            this.eventAggregator = AppBootstrapper.container.GetInstance<IEventAggregator>();
            eventAggregator.Subscribe(this);

            this.eventAggregator.PublishOnUIThread(new GUIReadyMessage());
        }

        public void Handle(AllergoloCiboMessage message)
        {
            Title.Text = message.ingredient.nome;
            Description.Text = message.ingredient.descrizione;
            //Immagine.Source = new BitmapImage(new Uri(cibiUri + message.ingredient.immagine));
        }

        public void Handle(AllergoloAllergiaMessage message)
        {
            Title.Text = message.allergia.nome;
            Description.Text = message.allergia.descrizione;
            Immagine.Source = new BitmapImage(new Uri(allergieUri + message.allergia.immagine));
        }


    }
}
