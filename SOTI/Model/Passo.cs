namespace SOTI.Model
{
    public class Passo
    {
        public int idRicetta { set; get; }
        public int ordine { set; get; }
        public bool passoDoppio { set; get; }
        public Cibo ciboGiusto { set; get; }
        public Cibo ciboSbagliato { set; get; }
    }
}