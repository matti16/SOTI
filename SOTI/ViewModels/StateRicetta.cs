using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    public class StateRicetta
    {
        private Ricetta ricetta;

        private int passo;

        public void initRicetta(Ricetta ricetta)
        {
            this.ricetta = ricetta;
            this.passo = 0;
        }

        public void MoveNext()
        {
            if (HasNext())
                passo++;
        }

        public bool HasNext()
        {
            return passo < ricetta.passi.Count - 1;
        }

        public Passo PassoCorrente { get { return this.ricetta.passi[passo]; } }
    }
}
