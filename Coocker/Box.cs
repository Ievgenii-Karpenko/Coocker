using System;
using System.Diagnostics;

namespace Coocker
{
    [DebuggerDisplay("{Product}, {Count}")]
    [Serializable]
    public class Box
    {
        public Product Product { get; set; }
        public int Count { get; set; }

        private Box() { }

        public Box(Product product, int count)
        {
            Product = product;
            Count = count;
        }
    }
}
