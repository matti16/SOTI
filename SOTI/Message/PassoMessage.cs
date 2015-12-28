using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class PassoMessage
    {
        public bool doble { get; private set; }
        public Cibo first { get; private set; }
        public Cibo second { get; private set; }

        public PassoMessage(bool doble, Cibo first, Cibo second)
        {
            this.doble = doble;
            this.first = first;
            this.second = second;
        }

    }
}
