using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class WrongProductMessage
    {
        public Cibo product { get; private set; }
        public Allergia allergia_1 { get; private set; }
        public Allergia allergia_2 { get; private set; }

        public WrongProductMessage(Cibo product, Allergia uno, Allergia due)
        {
            this.product = product;
            this.allergia_1 = uno;
            this.allergia_2 = due;
        }
    }
}
