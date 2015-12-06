namespace SOTI.Model
{
    public class Cibo
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public bool uovo { get; set; }
        public bool latte { get; set; }
        public bool pesce { get; set; }
        public bool frutta { get; set; }
        public string immagine { get; set; }
        public int prezzo { get; set; }
    }
}