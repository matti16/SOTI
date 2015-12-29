using SOTI.ViewModels.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class ScontrinoMessage
    {
        public List<Purchase> list { get; private set; }
        public int tot { get; private set; }

        public ScontrinoMessage(List<Purchase> list, int tot)
        {
            this.list = list;
            this.tot = tot;
        }

    }

}
