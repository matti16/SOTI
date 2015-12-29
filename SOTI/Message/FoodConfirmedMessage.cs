using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class FoodConfirmedMessage
    {
        public bool confirmed { get; private set; }

        public FoodConfirmedMessage(bool confirmed)
        {
            this.confirmed = confirmed;
        }
    }
}
