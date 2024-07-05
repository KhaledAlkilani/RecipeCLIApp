using RecipeCLIApp.Interface;
using RecipeCLIApp.Model;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RecipeCLIApp.Service
{
    public class RecipeService : IRecipeService
    {

        private List<Recipe> _recipes;

        public RecipeService()
        {

            //_recipes = RecipeData.GetRecipes();
            _recipes = LoadRecipesFromJson("recipes.json");
        }

        private List<Recipe> LoadRecipesFromJson(string filePath)
        {
            try
            {
                // Read the file content in one go
                string json = File.ReadAllText(filePath);
                // Deserialize the JSON directly into a List of Recipe
                var recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
                // Return the list, ensuring it is never null
                return recipes ?? new List<Recipe>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load recipes from JSON file: {ex.Message}");
            }
            return new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Provided recipe is null.");
            }
            int nextId = _recipes.Any() ? _recipes.Max(r => r.Id) + 1 : 1;
            recipe.Id = nextId;
            _recipes.Add(recipe);
        }

        public List<Recipe> GetAllRecipes()
        {
            var recipesList = _recipes;
            return recipesList;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _recipes.FirstOrDefault(r => r.Id == recipeId) ?? throw new Exception($"No recipe found with ID: {recipeId}");
        }

        public void RemoveRecipe(int recipeId)
        {
           _recipes.RemoveAll(x => x.Id == recipeId);
        }

        public List<Recipe> searchRecipe(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
            {
                throw new ArgumentException("Search criteria cannot be null or empty.", nameof(criteria));
            }

            return _recipes.Where(r => (r.Name?.Contains(criteria) ?? false) || (r.Category?.Contains(criteria) ?? false)).ToList();

        }
    }
}
