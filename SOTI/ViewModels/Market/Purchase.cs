namespace SOTI.ViewModels.Market
{
    public class Purchase
    {
        public Purchase(string product, int quantity, int price)
        {
            this.product = product;
            this.quantity = quantity;
            this.price = price;
        }

        public string product { get; private set; }
        public int quantity { get; private set; }
        public int price { get; private set; }


    }
}