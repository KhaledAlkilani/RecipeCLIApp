﻿using DotNetEnv;
using Npgsql;
using RecipeCLIApp.Model;
using RecipeCLIApp.Repositories;
using RecipeCLIApp.Service;

class Program
{
    static void Main(string[] args)
    {

        // Load environment variables from the .env file
        Env.Load();

        // Retrieve the connection string from the environment variables
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Error: Connection string is not set.");
            return;
        }

        var recipeRepository = new RecipeRepository(connectionString);
        var recipeService = new RecipeService(recipeRepository);

        Console.WriteLine("Welcome to Recipe CLI App!");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option by entering the option number:\n");
            Console.WriteLine("1: List all recipes.");
            Console.WriteLine("2: Get a recipe by its ID.");
            Console.WriteLine("3: Add a recipe.");
            Console.WriteLine("4: Update a recipe.");
            Console.WriteLine("5: Remove a recipe by entering the recipe ID.");
            Console.WriteLine("6: Search a recipe by name or category.");
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
                        Console.WriteLine($"ID: {recipe.Id}. Name: {recipe.Name}. Category: {recipe.Category}");
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
                            Console.WriteLine($"{recipeById.Id}. {recipeById.Name}.");
                            Console.WriteLine();
                            Console.WriteLine(">>>Category<<<");
                            Console.WriteLine($"{recipeById.Category}.");
                            Console.WriteLine();
                            Console.WriteLine(">>>Ingredients<<<");

                            foreach (var ingredient in recipeById.Ingredients ?? throw new Exception())
                            {
                                Console.WriteLine($"- {ingredient}");
                            }

                            Console.WriteLine();
                            Console.WriteLine(">>>Instructions<<<");

                            foreach (var instruction in recipeById.Instructions ?? throw new Exception())
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
                    if (newRecipe != null)
                    {
                        try
                        {
                            recipeService.AddRecipe(newRecipe);
                            Console.WriteLine("Recipe added successfully!");
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No new recipe was added.");
                    }
                    break;

                case "4":

                    UpdateRecipe(recipeService);
                    break;

                case "5":

                    int recipeIdToRemove = RemoveRecipeByUser(recipeService);

                    if (recipeIdToRemove != -1)
                    {
                        try
                        {
                            recipeService.RemoveRecipe(recipeIdToRemove);
                            Console.WriteLine("Recipe deleted successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                            Console.WriteLine("Failed to remove recipe.");
                        }
                    }

                    break;

                case "6":

                    var criteria = GetSearchCriteria();

                    try
                    {
                        var searchResults = recipeService.SearchRecipe(criteria);
                        if (searchResults.Any())
                        {
                            Console.WriteLine("Search Results:");
                            foreach (var recipe in searchResults)
                            {
                                Console.WriteLine($"Name: {recipe.Name}. Category: {recipe.Category}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No recipes found matching your criteria.");
                        }

                    }
                    catch (Exception ex)
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
                string continueResponse = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();

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

        //____________________________________________________

        static Recipe GetRecipeFromUser()
        {
            string recipeName = "";
            string recipeCategory = "";

            while (true)
            {
                Console.Write("Enter the recipe name: ");
                recipeName = Console.ReadLine() ?? throw new Exception();

                if (string.IsNullOrWhiteSpace(recipeName))
                {
                    Console.WriteLine("Recipe name cannot be empty. Please enter a valid name.");
                }
                else
                {
                    break;
                }
            }
        

            while (true)
            {
                Console.Write("Enter the recipe category: ");
                recipeCategory = Console.ReadLine() ?? throw new Exception();

                if (string.IsNullOrWhiteSpace(recipeCategory))
                {
                    Console.WriteLine("Recipe category cannot be empty. Please enter a valid category.");
                }
                else
                {
                    break;
                }
            }
           
            List<string> ingredients = new List<string>();
            Console.WriteLine("Enter ingredients (press 'Enter' then type 'done' to finish):");

            while (true)
            {
                string ingredient = Console.ReadLine() ?? throw new Exception("Ingredient cannot be null.");
                if (ingredient.Equals("done", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!string.IsNullOrWhiteSpace(ingredient))
                {
                    ingredients.Add(ingredient);
                }
            }

            List<string> instructions = new List<string>();
            Console.WriteLine("Enter instructions (press 'Enter' then type 'done' to finish):");

            while (true)
            {
                string instruction = Console.ReadLine() ?? throw new Exception("Instructions cannot be null.");
                if (instruction.Equals("done", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!string.IsNullOrWhiteSpace(instruction))
                {
                    instructions.Add(instruction);
                }
            }

            var isGlutenFree = GetDietaryPreference("Is the recipe gluten-free? (Y/N) or press Enter to skip. ");
            var isDairyFree = GetDietaryPreference("Is the recipe dairy-free? (Y/N) or press Enter to skip. ");
            var isVegan = GetDietaryPreference("Is recipe vegan? (Y/N) or press Enter to skip. ");

            Recipe newRecipe = new Recipe
            {
                Name = recipeName,
                Category = recipeCategory,
                Ingredients = ingredients,
                Instructions = instructions,
                IsGlutenFree = isGlutenFree,
                IsDairyFree = isDairyFree,
                IsVegan = isVegan,
            };

            while (true)
            {
                Console.WriteLine("Are you sure you want to add this recipe? (Y/N)");
                string recipeAddConfirmation = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();

                if (recipeAddConfirmation == "Y")
                {
                    return newRecipe;

                }
                else if (recipeAddConfirmation == "N")
                {
                    Console.WriteLine("Recipe addition cancelled.");
                    return null;
                }
                else
                {
                    Console.WriteLine("Invalid answer, please answer Y or N");
                }
            }
        };

        static bool? GetDietaryPreference(string prompt)
        {
            Console.Write(prompt);
            string response;

            while (true)
            {
                response = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }
                else if (response == "Y")
                {
                    return true;
                } else if (response == "N")
                {
                    return false;
                } else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                }
            }
        }

        static void UpdateRecipe(RecipeService recipeService)
        {
            Console.WriteLine("Search for a recipe to update.");
            string criteria = GetSearchCriteria();
            var recipes = recipeService.SearchRecipe(criteria);

            if (!recipes.Any())
            {
                Console.WriteLine("No matching recipes found.");
                return;
            }

            foreach (var recipe in recipes)
            {
                Console.WriteLine($"ID: {recipe.Id}. Name: {recipe.Name}. Category: {recipe.Category}.");
            }

            Console.WriteLine("Enter the ID of the recipe you want to update:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var recipeToUpdate = recipes.FirstOrDefault(r => r.Id == id);
            if (recipeToUpdate == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            // Update name and category
            Console.WriteLine($"Current Name: {recipeToUpdate.Name}");
            Console.Write("Enter new name (or press enter to skip): ");
            string newName = Console.ReadLine() ?? throw new Exception();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                recipeToUpdate.Name = newName;
            }

            Console.WriteLine($"Current Category: {recipeToUpdate.Category}");
            Console.Write("Enter new category (or press enter to skip): ");
            string newCategory = Console.ReadLine() ?? throw new Exception();
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                recipeToUpdate.Category = newCategory;
            }

            // Update dietary information
            recipeToUpdate.IsGlutenFree = GetDietaryPreference("Is the recipe gluten-free? (Y/N) or press Enter to skip. ");
            recipeToUpdate.IsDairyFree = GetDietaryPreference("Is the recipe dairy-free? (Y/N) or press Enter to skip. ");
            recipeToUpdate.IsVegan = GetDietaryPreference("Is the recipe vegan? (Y/N) or press Enter to skip. ");

            // Update ingredients
            Console.WriteLine("Current Ingredients:");
            int ingredientIndex = 1;
            foreach (var ingredient in recipeToUpdate.Ingredients ?? throw new Exception())
            {
                Console.WriteLine($"{ingredientIndex++}: {ingredient}");
            }
            Console.WriteLine("Type 'add' to add a new ingredient or the number to delete an ingredient.");
            string ingredientResponse = Console.ReadLine()?.ToLower() ?? throw new Exception();
            if (ingredientResponse == "add")
            {
                Console.WriteLine("Enter new ingredient:");
                string newIngredient = Console.ReadLine() ?? throw new Exception();
                recipeToUpdate.Ingredients.Add(newIngredient);
            }
            else if (int.TryParse(ingredientResponse, out int ingredientNumber) && ingredientNumber <= recipeToUpdate.Ingredients.Count)
            {
                recipeToUpdate.Ingredients.RemoveAt(ingredientNumber - 1);
                Console.WriteLine("Ingredient removed.");
            }

            // Update instructions
            Console.WriteLine("Current Instructions:");
            int instructionIndex = 1;
            foreach (var instruction in recipeToUpdate.Instructions ?? throw new Exception())
            {
                Console.WriteLine($"{instructionIndex++}: {instruction}");
            }
            Console.WriteLine("Type 'add' to add a new instruction or the number to delete an instruction.");
            string instructionResponse = Console.ReadLine()?.ToLower() ?? throw new Exception();
            if (instructionResponse == "add")
            {
                Console.WriteLine("Enter new instruction:");
                string newInstruction = Console.ReadLine() ?? throw new Exception();
                recipeToUpdate.Instructions.Add(newInstruction);
            }
            else if (int.TryParse(instructionResponse, out int instructionNumber) && instructionNumber <= recipeToUpdate.Instructions.Count)
            {
                recipeToUpdate.Instructions.RemoveAt(instructionNumber - 1);
                Console.WriteLine("Instruction removed.");
            }

            // Saving the updated recipe
            while (true)
            {
                Console.WriteLine("Are you sure you want to update this recipe? (Y/N)");
                string recipeUpdateConfirmation = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();
                

                if (recipeUpdateConfirmation == "Y")
                {
                    recipeService.UpdateRecipe(recipeToUpdate.Id, recipeToUpdate);
                    Console.WriteLine("Recipe updated successfully.");
                    break;

                }
                else if (recipeUpdateConfirmation == "N")
                {
                    Console.WriteLine("Recipe update cancelled.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid answer, please answer Y or N");
                }
            }           
        }

        static int RemoveRecipeByUser(RecipeService recipeService)
        {
            Console.Write("Enter Recipe ID: ");

            if (!int.TryParse(Console.ReadLine(), out int recipeIdToBeRemoved))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return -1;
            }

            var recipeToRemove = recipeService.GetRecipeById(recipeIdToBeRemoved);
            if (recipeToRemove == null)
            {
                Console.WriteLine("Recipe not found.");
                return -1;
            }

            Console.WriteLine(">>>Recipe ID and Name<<<");
            Console.WriteLine($"ID: {recipeToRemove.Id}. Name: {recipeToRemove.Name}.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Are you sure you want to delete the selected recipe? (Y/N): ");
                string removeRecipeConfirmation = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();

                if (string.IsNullOrEmpty(removeRecipeConfirmation))
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                    continue;
                }

                if (removeRecipeConfirmation == "Y")
                {
                    return recipeIdToBeRemoved;
                }
                else if (removeRecipeConfirmation == "N")
                {
                    Console.WriteLine("Deletion cancelled.");
                    return -1;
                }
                else
                {
                    Console.WriteLine("Invalid answer, please answer Y or N");
                }
            }
        }

        static string GetSearchCriteria()
        {
            string criteria = "";

            while (true)
            {
                Console.Write("Enter recipe name or category: ");
                criteria = Console.ReadLine()?.Trim().ToUpper() ?? throw new Exception();

                if (!string.IsNullOrEmpty(criteria))
                {
                    break;
                }

                Console.WriteLine("Input cannot be empty. Please enter a valid recipe name or category.");
            }

            return criteria;
        }

    }
}



