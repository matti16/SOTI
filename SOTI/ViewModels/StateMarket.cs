using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    public class StateMarket
    {
        public Allergia Allergia_1 { get; private set; }
        public Allergia Allergia_2 { get; private set; }
        public Cibo Readed_food { get; private set; } 
        public Dictionary<Cibo, int> List_of_Products { get; private set; }

        public void InitMarket(Allergia first, Allergia second)
        {
            List_of_Products = new Dictionary<Cibo, int>();
            Allergia_1 = first;
            Allergia_2 = second;
        }

        public void ReadedFood(Cibo food)
        {
            Readed_food = food;
        }

        public void AddToList(Cibo food)
        {
            if (List_of_Products.ContainsKey(food))
            {
                List_of_Products[food]++;
            }
            else
            {
                List_of_Products.Add(food, 1);
            }
        }
    }
}
