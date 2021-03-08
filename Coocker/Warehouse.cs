using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Coocker
{
    [Serializable]
    public class Warehouse
    {
        [XmlArrayItem(nameof(Wardrobe))]
        public List<Wardrobe> Wardrobes { get; set; } = new List<Wardrobe>();

        public List<Box> GetAllProducts()
        {
            List<Box> productBoxes = new List<Box>();
            foreach (var wardrobe in Wardrobes)
            {
                productBoxes.AddRange(wardrobe.Boxes);
            }

            return productBoxes;
        }
    }
}
