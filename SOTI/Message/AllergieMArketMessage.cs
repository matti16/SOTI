using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class AllergieMarketMessage
    {
        public Allergia allergia_1 { get; private set; }
        public Allergia allergia_2 { get; private set; }

        public AllergieMarketMessage(Allergia first, Allergia second)
        {
            allergia_1 = first;
            allergia_2 = second;
        }
    }
}
