using SOTI.Model;

namespace SOTI.Message
{
    public class AllergoloCiboMessage
    {
        public Cibo ingredient { get; private set; }

        public AllergoloCiboMessage(Cibo ingredient)
        {
            this.ingredient = ingredient;
        }
    }
}