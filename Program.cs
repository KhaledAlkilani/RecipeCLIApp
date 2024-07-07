using RecipeCLIApp.Model;
using RecipeCLIApp.Service;

class Program
{
    static void Main(string[] args)
    {

        var recipeService = new RecipeService();
        Console.WriteLine("Welcome to Recipe CLI App!");

    static Recipe GetRecipeFromUser()
        {
            Console.Write("Enter the recipe name: ");
            string recipeName = Console.ReadLine() ?? throw new Exception();

            Console.Write("Enter the recipe category: ");
            string recipeCategory = Console.ReadLine() ?? throw new Exception();

            List<string> ingredients = new List<string>();
            string ingredient;

            do
            {
                Console.Write("Enter an ingredient: ");
                ingredient = Console.ReadLine() ?? throw new Exception();

                if (!string.IsNullOrWhiteSpace(ingredient))
                {
                    ingredients.Add(ingredient);
                }
            }

            while (!string.IsNullOrWhiteSpace(ingredient));

            List<string> instructions = new List<string>();
            string instruction;

            do
            {
                Console.Write("Enter an instruction: ");
                instruction = Console.ReadLine() ?? throw new Exception();

                if (!string.IsNullOrWhiteSpace(instruction))
                {
                    instructions.Add(instruction);
                }
            }

            while (!string.IsNullOrWhiteSpace(instruction));

            //Console.Write("Is recipe zero-gluten? ");
            //string isGluten = Console.ReadLine() ?? throw new Exception();

            return new Recipe
            {
                Name = recipeName,
                Category = recipeCategory,
                Ingredients = ingredients,
                Instructions = instructions
            };
        };

        static string GetSearchCriteria()
        {

            Console.Write("Enter recipe name or category: ");
            string criteria = Console.ReadLine().Trim().ToUpper() ?? throw new Exception();

            return criteria;
           
        }

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
            Console.Write("Type the option number here: ");
            var option = Console.ReadLine();
            Console.Write($"Selected Option: {option}\n");
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    var recipes = recipeService.GetAllRecipes();
                    Console.WriteLine(">>>Available Recipes<<<");
                    foreach (var recipe in recipes)
                    {
                        Console.WriteLine($"ID: {recipe.Id}, Name: {recipe.Name}, Category: {recipe.Category}");
                    }
                    break;

                case "2":
                    Console.Write("Enter Recipe ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int recipeIdToBeGotten))
                    {
                        Console.WriteLine("Invalid ID. Please enter a number.");
                        break;
                    }

                    try
                    {
                        var recipeById = recipeService.GetRecipeById(recipeIdToBeGotten);

                        if (recipeById != null)
                        {

                            Console.WriteLine();
                            Console.WriteLine(">>>Recipe ID and Name<<<");
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
                            Console.WriteLine(">>>Nutrition Information<<<");
                            Console.WriteLine($"- Is gluten free? {recipeById.IsGlutenFree}");
                            Console.WriteLine($"- Is dairy free? {recipeById.IsDairyFree}");
                            Console.WriteLine($"- Is vegan free? {recipeById.IsVegan}");
                        }
                        else
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                    }

                    catch (Exception ex)

                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine("Recipe not found or there was an issue retrieving it.");
                    }
                    break;

                case "3":

                    Recipe newRecipe = GetRecipeFromUser();
                    
                    try
                    {
                        recipeService.AddRecipe(newRecipe);
                        Console.WriteLine("Recipe added successfully!");
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                    }
                    break;

                case "4":

                    Console.Write("Enter Recipe ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int recipeIdToBeRemoved))
                    {
                        Console.WriteLine("Invalid ID. Please enter a number.");
                        break;
                    }

                    try
                    {
                        var recipeToRemove = recipeService.GetRecipeById(recipeIdToBeRemoved);
                        if (recipeToRemove == null)
                        {
                            Console.WriteLine("Recipe not found.");
                            break;
                        }

                        Console.WriteLine(">>>Recipe ID and Name<<<");
                        Console.WriteLine($"{recipeToRemove.Id}: {recipeToRemove.Name}");
                        Console.WriteLine();
                        Console.Write("Are you sure you want to delete the selected recipe? (Y/N): ");
                        string removeRecipeConfirmation = Console.ReadLine().Trim().ToUpper();

                        if (removeRecipeConfirmation == "Y")
                        {
                            recipeService.RemoveRecipe(recipeIdToBeRemoved);
                            Console.WriteLine("Recipe has been removed successfully!");
                        }

                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                    }

                    catch (Exception ex)

                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine("Failed to remove recipe.");
                    }

                    break;

                    case "5":

                    var criteria = GetSearchCriteria();

                    try
                    {
                        var searchResults = recipeService.SearchRecipe(criteria);
                        if (searchResults.Any())
                        {
                            Console.WriteLine("Search Results:");
                            foreach (var recipe in searchResults)
                            {
                                Console.WriteLine($"Name: {recipe.Name}, Category: {recipe.Category}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No recipes found matching your criteria.");
                        }

                    } catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during the search: {ex.Message}");
                    }

                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }

            Console.WriteLine();

            bool validResponse = false;

            while (!validResponse)
            {
                Console.Write("Do you want to continue? (Y/N): ");
                string continueResponse = Console.ReadLine().Trim().ToUpper();

                if (continueResponse == "Y")
                {
                    validResponse = true;
                }
                else if (continueResponse == "N")
                {
                    Console.WriteLine("Bye bye");
                    Console.WriteLine("The app will close in 3 seconds");
                    Thread.Sleep(3000);
                    return; 
                }
                else
                {
                    Console.WriteLine("Invalid answer, please answer Y or N.");
                }
            }

        }
    }
}


