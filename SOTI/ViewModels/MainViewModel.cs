using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IEventAggregator eventAggregator;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}