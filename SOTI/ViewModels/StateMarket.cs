using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    public class StateMarket
    {
        public Allergia Allergia_1 { get; private set; }
        public Allergia Allergia_2 { get; private set; }

        public void InitMarket(Allergia first, Allergia second)
        {
            Allergia_1 = first;
            Allergia_2 = second;
        }
    }
}
