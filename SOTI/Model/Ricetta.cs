using System.Collections.Generic;

namespace SOTI.Model
{
    public class Ricetta
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string allergia { get; set; }
        public string immagine { get; set; }
        public List<Passo> passi = new List<Passo>();


        public override string ToString()
        {
            string value = id.ToString();
            value += ". " + nome.ToString() + ": \n";
            for (int i = 0; i < passi.Count; i++)
            {
                value += (1+i).ToString() + ") " + passi[i].ciboGiusto.nome + "\n";
            }
            return value;
        }


    } 

   
}