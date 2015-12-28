using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOTI.Model;

namespace SOTI.Message
{
    public class AllergoloAllergiaMessage
    {
        public Allergia allergia { get; private set; }

        public AllergoloAllergiaMessage(Allergia allergia)
        {
            this.allergia = allergia;
        }
    }
}
