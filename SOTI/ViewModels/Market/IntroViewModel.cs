using Caliburn.Micro;
using SOTI.Message;
using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels.Market
{
    class IntroViewModel : BaseGameScreenViewModel
    {

        /// <summary>
        /// Costruttore della classe, per far funzionare il tutto è necessario chiamare anche il costruttore della classe base
        /// </summary>
        /// <param name="eventAggregator"></param>
        public IntroViewModel(IEventAggregator eventAggregator, DataLayer data, StateMarket state) : base(eventAggregator)
        {
            Random rnd = new Random();
            List<Allergia> allergie = data.allergie;
            int first = rnd.Next(allergie.Count);
            Allergia firstAllergia = allergie[first];

            allergie.RemoveAt(first);
            int second = rnd.Next(allergie.Count);
            Allergia secondAllergia = allergie[second];

            state.InitMarket(firstAllergia, secondAllergia);

        }
        
    }
}
