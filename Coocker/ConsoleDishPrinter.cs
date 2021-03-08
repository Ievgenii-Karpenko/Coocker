using System;
using System.Collections.Generic;
using System.Text;

namespace Coocker
{
    class ConsoleDishPrinter : IDishesPrinter
    {
        private Dictionary<Dish, int> doneDishes;

        public ConsoleDishPrinter(Dictionary<Dish, int> dishes)
        {
            doneDishes = dishes;
        }

        public void PrintDishes()
        {
            foreach (var dish in doneDishes)
            {
                Console.WriteLine($"{dish.Key}; \tAmount: {dish.Value}");
            }
        }
    }
}
