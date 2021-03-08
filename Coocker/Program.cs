using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Coocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Warehouse warehouse = CreateFromFile<Warehouse>(@"Data\Warehouse.xml");
            Cookbook cookbook = CreateFromFile<Cookbook>(@"Data\Cookbook.xml");
            DishStrategy strategy = ReadStrategyFormArgs(args);

            DishMaker maker = new DishMaker(cookbook, warehouse.GetAllProducts(), strategy);
            Dictionary<Dish, int> doneDishes = maker.MakeDishes();

            IDishesPrinter printer = new ConsoleDishPrinter(doneDishes);
            printer.PrintDishes();

            Console.ReadLine();
        }

        private static DishStrategy ReadStrategyFormArgs(string[] args)
        {
            foreach (var item in args)
            {
                if(item.Contains("mass", StringComparison.InvariantCultureIgnoreCase))
                    return DishStrategy.Mass;
                if (item.Contains("calories", StringComparison.InvariantCultureIgnoreCase))
                    return DishStrategy.Calories;
                if (item.Contains("products", StringComparison.InvariantCultureIgnoreCase))
                    return DishStrategy.Products;
            }
            return DishStrategy.NotSet;
        }

        private static T CreateFromFile<T>(string path)
        {
            T obj = default(T);
            if (!File.Exists(path))
                return obj;

            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                obj = (T)formatter.Deserialize(fs);
            }

            return obj;
        }
    }
}
