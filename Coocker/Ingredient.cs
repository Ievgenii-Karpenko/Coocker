using System;
using System.Collections.Generic;
using System.Text;

namespace Coocker
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public Ingredient(string name, int count)
        {
            Name = name;
            Amount = count;
        }

        private Ingredient() { }
    }
}
