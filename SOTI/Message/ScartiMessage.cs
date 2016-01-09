using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class ScartiMessage
    {
        public List<Cibo> scarti { get; private set; }

        public ScartiMessage(List<Cibo> scarti)
        {
            this.scarti = scarti;
        }
    }
}
