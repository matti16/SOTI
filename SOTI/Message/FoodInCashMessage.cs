using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class FoodInCashMessage
    {
        public Cibo food { get; private set; }

        public FoodInCashMessage(Cibo food)
        {
            this.food = food;
        }
    }
}
