using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Coocker
{
    [Serializable]
    public class Dish
    {
        public string Name { get; set; }
        [XmlArrayItem(nameof(Ingredient))]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int Weight { get; set; }
        public int Calories { get; set; }

        public override string ToString()
        {
            return $"{Name}: Weight - {Weight}, Calories - {Calories}";
        }
    }
}
