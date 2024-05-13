using System;
using System.Collections.Generic;

namespace RecipeApp
{
    // Enum to represent different food groups
    enum FoodGroup
    {
        Fruits,
        Vegetables,
        Grains,
        Protein,
        Dairy,
        Fats_Oils,
        Sugars_Sweets
    }

    // Class to represent an ingredient
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public FoodGroup Group { get; set; }
    }

    // Class to represent a step in a recipe
    class Step
    {
        public string Description { get; set; }
    }

    // Class to represent a recipe
    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }

        // Method to add an ingredient to the recipe
        public void AddIngredient(string name, double quantity, string unit, int calories, FoodGroup group)
        {
            Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, Group = group });
        }

        // Method to add a step to the recipe
        public void AddStep(string description)
        {
            Steps.Add(new Step { Description = description });
        }

        // Method to display the recipe
        public void DisplayRecipe()
        {
            Console.WriteLine($"Recipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }
            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i].Description}");
            }
        }

        // Method to calculate the total calories of all ingredients in the recipe
        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }
            return totalCalories;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        // Method to display all recipes in alphabetical order by name
        static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        // Method to allow the user to choose a recipe and display it
        static void ChooseRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Choose a recipe:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= recipes.Count)
            {
                recipes[choice - 1].DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Recipe App!");

            while (true)
            {
                Console.WriteLine("Enter option:");
                Console.WriteLine("1. Add recipe");
                Console.WriteLine("2. Display all recipes");
                Console.WriteLine("3. Choose a recipe");
                Console.WriteLine("4. Exit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Enter recipe name:");
                        string recipeName = Console.ReadLine();
                        Recipe recipe = new Recipe(recipeName);

                        Console.WriteLine("Enter the number of ingredients:");
                        int numIngredients = int.Parse(Console.ReadLine());

                        for (int i = 0; i < numIngredients; i++)
                        {
                            Console.WriteLine($"Enter details for ingredient {i + 1}:");
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Quantity: ");
                            double quantity = double.Parse(Console.ReadLine());
                            Console.Write("Unit: ");
                            string unit = Console.ReadLine();
                            Console.Write("Calories: ");
                            int calories = int.Parse(Console.ReadLine());
                            Console.Write("Food Group (Fruits, Vegetables, Grains, Protein, Dairy, Fats_Oils, Sugars_Sweets): ");
                            FoodGroup group = (FoodGroup)Enum.Parse(typeof(FoodGroup), Console.ReadLine(), true);

                            recipe.AddIngredient(name, quantity, unit, calories, group);
                        }

                        Console.WriteLine("Enter the number of steps:");
                        int numSteps = int.Parse(Console.ReadLine());

                        for (int i = 0; i < numSteps; i++)
                        {
                            Console.WriteLine($"Enter step {i + 1}:");
                            string description = Console.ReadLine();
                            recipe.AddStep(description);
                        }

                        recipes.Add(recipe);
                        break;

                    case "2":
                        DisplayAllRecipes();
                        break;

                    case "3":
                        ChooseRecipe();
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
