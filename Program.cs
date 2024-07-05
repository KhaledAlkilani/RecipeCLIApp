using RecipeCLIApp.Service;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Welcome to Recipe CLI App!");

        var recipeService = new RecipeService();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option by entering the option number:\n");
            Console.WriteLine("1: List all recipes.");
            Console.WriteLine("2: Get a recipe by its ID.");
            Console.WriteLine("3: Add a recipe.");
            Console.WriteLine("4: Remove a recipe by entering the recipe ID.");
            Console.WriteLine("5: Search a recipe by name or category.");
            Console.WriteLine();
            var option = Console.ReadLine();
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    var recipes = recipeService.GetAllRecipes();
                    foreach (var recipe in recipes)
                    {
                        Console.WriteLine($"{recipe.Id}: {recipe.Name}");
                    }
                    break;
                case "2":
                    Console.Write("Enter Recipe ID: ");

                    int id = int.Parse(Console.ReadLine());
                    var recipeById = recipeService.GetRecipeById(id);

                    if (recipeById != null)
                    {

                        Console.WriteLine();
                        Console.WriteLine(">>>Recipe ID and name<<<");
                        Console.WriteLine($"{recipeById.Id}: {recipeById.Name}");
                        Console.WriteLine();
                        Console.WriteLine(">>>Category<<<");
                        Console.WriteLine($"{recipeById.Category}");
                        Console.WriteLine();
                        Console.WriteLine(">>>Ingredients<<<");

                        foreach (var ingredient in recipeById.Ingredients)
                        {
                            Console.WriteLine($"- {ingredient}");
                        }

                        Console.WriteLine();
                        Console.WriteLine(">>>Instructions<<<");

                        foreach (var instruction in recipeById.Instructions)
                        {
                            Console.WriteLine($"- {instruction}");
                        }
                        Console.WriteLine();
                        Console.WriteLine(">>>Nutrition information<<<");
                        Console.WriteLine($"- Is gluten? {recipeById.IsGlutenFree}");
                        Console.WriteLine($"- Is dairy? {recipeById.IsDairyFree}");
                        Console.WriteLine($"- Is vegan? {recipeById.IsVegan}");
                    }
                    else
                    {
                        Console.WriteLine("Recipe not found.");
                    }
                    break;

                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }

            Console.WriteLine();
            Console.Write("Do you want to continue? (Y/N): ");
            string continueResponse = Console.ReadLine().Trim().ToUpper();

            if (continueResponse != "Y")
            {
                Console.WriteLine("Byebye");
                Console.WriteLine("The app will close in 3 seconds");
                Thread.Sleep(3000);
                return;
            }

        }
    }
}