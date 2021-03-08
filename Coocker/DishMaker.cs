using System;
using System.Collections.Generic;
using System.Linq;

namespace Coocker
{
    public enum DishStrategy
    {
        NotSet,
        Mass,
        Calories,
        Products
    }

    public class DishMaker
    {
        public DishStrategy Strategy { get; } = DishStrategy.NotSet;
        public Cookbook Cookbook { get; }
        public List<Box> ProductBoxes { get; }

        public DishMaker(Cookbook cookbook, List<Box> boxes, DishStrategy strategy = DishStrategy.NotSet)
        {
            Strategy = strategy;
            Cookbook = cookbook;
            ProductBoxes = boxes;
        }

        public Dictionary<Dish, int> MakeDishes()
        {
            Dictionary<Dish, int> result = null;
            List<Dish> sortedRecipes = Cookbook.Recipes;
            switch (Strategy)
            {
                case DishStrategy.NotSet:
                    break;
                case DishStrategy.Calories:
                    sortedRecipes = Cookbook.Recipes.OrderByDescending(r => r.Calories).ToList();
                    break;
                case DishStrategy.Mass:
                    sortedRecipes = Cookbook.Recipes.OrderByDescending(r => r.Weight).ToList();
                    break;
                case DishStrategy.Products:
                    sortedRecipes = Cookbook.Recipes.OrderByDescending(r => r.Ingredients.Count).ToList();
                    break;
            }
            result = MakeDishes(sortedRecipes);
            return result;
        }


        private Dictionary<Dish, int> MakeDishes(IEnumerable<Dish> recipes)
        {
            var result = new Dictionary<Dish, int>();
            foreach (var recipe in recipes)
            {
                List<Box> boxesForRecipe = GetBoxesWithIngridients(recipe);
                if (boxesForRecipe.Count == recipe.Ingredients.Count)
                {
                    int dishesCount = UsePreparedProducts(boxesForRecipe, recipe.Ingredients);
                    AddDishToResult(result, recipe, dishesCount);
                }
            }

            return result;
        }

        private List<Box> GetBoxesWithIngridients(Dish recipe)
        {
            List<Box> boxesForRecipe = new List<Box>();
            foreach (var item in recipe.Ingredients)
            {
                Box productFromBoxes = ProductBoxes.FirstOrDefault(p => p.Product.Name == item.Name);
                if (productFromBoxes?.Count >= item.Amount)
                {
                    boxesForRecipe.Add(productFromBoxes);
                }
            }
            return boxesForRecipe;
        }

        private void AddDishToResult(Dictionary<Dish, int> result, Dish dish, int dishesCount)
        {
            if (result.ContainsKey(dish))
                result[dish] += dishesCount;
            else
                result.Add(dish, dishesCount);
        }

        private int UsePreparedProducts(List<Box> boxesForRecipe, List<Ingredient> ingredients)
        {
            Dictionary<string, int> amountOfPortions = CalculateAmountOfPortionsForIngredients(boxesForRecipe, ingredients);
            int amountOfDish = CalculateAmountOfDishesToCoock(amountOfPortions);

            foreach (var item in ingredients)
            {
                Box productFromBoxes = boxesForRecipe.First(p => p.Product.Name == item.Name);
                productFromBoxes.Count -= amountOfDish * item.Amount;
            }
            return amountOfDish;
        }

        private int CalculateAmountOfDishesToCoock(Dictionary<string, int> amountOfPortions)
        {
            switch (Strategy)
            {
                case DishStrategy.Mass:
                case DishStrategy.Calories:
                case DishStrategy.Products:
                    return amountOfPortions.Min(p => p.Value);
                default:
                    return 1;
            }
        }

        private Dictionary<string, int> CalculateAmountOfPortionsForIngredients(List<Box> boxesForRecipe, List<Ingredient> ingredients)
        {
            Dictionary<string, int> amountOfPortions = new Dictionary<string, int>();
            foreach (var ingredient in ingredients)
            {
                Box productFromBoxes = boxesForRecipe.First(p => p.Product.Name == ingredient.Name);
                int howManyPortionsHas = productFromBoxes.Count / ingredient.Amount;

                amountOfPortions.Add(ingredient.Name, howManyPortionsHas);
            }

            return amountOfPortions;
        }
    }
}
