using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Coocker
{
    [Serializable]
    public class Wardrobe
    {
        [XmlArrayItem(nameof(Box))]
        public List<Box> Boxes { get; set; } = new List<Box>();
    }

    [DebuggerDisplay("{Name}")]
    [Serializable]
    public class Product
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class Cookbook
    {
        [XmlArrayItem(nameof(Dish))]
        public List<Dish> Recipes { get; set; } = new List<Dish>();
    }
}
