using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class FoodReadedMessage
    {
        public FoodReadedMessage(string food)
        {
            this.Food = food;
        }

        public string Food { get; private set; }
    }
}
