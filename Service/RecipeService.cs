using RecipeCLIApp.Interface;
using RecipeCLIApp.Model;
using Newtonsoft.Json;

namespace RecipeCLIApp.Service
{
    public class RecipeService : IRecipeService
    {

        private List<Recipe> _recipes;
        private readonly string _jsonFilePath;

        public RecipeService()
        {
            _jsonFilePath = Path.Combine("C:\\Users\\mc120\\source\\repos\\RecipeCLIApp\\RecipeCLIApp\\recipes.json");
            _recipes = LoadRecipesFromJson(_jsonFilePath);
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
                return new List<Recipe>();
            }
        }

        private void SaveRecipesToJson(string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(_recipes, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to file: {ex}");
            }
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
            SaveRecipesToJson(_jsonFilePath);
        }

        public void UpdateRecipe(int recipeId, Recipe updatedRecipe)
        {
            var existingRecipe = _recipes.FirstOrDefault(r => r.Id == recipeId);
            if (existingRecipe == null)
            {
                throw new Exception("Recipe not found.");
            }

            existingRecipe.Name = updatedRecipe.Name;
            existingRecipe.Category = updatedRecipe.Category;
            existingRecipe.Ingredients = updatedRecipe.Ingredients;
            existingRecipe.Instructions = updatedRecipe.Instructions;
            existingRecipe.IsGlutenFree = updatedRecipe.IsGlutenFree;
            existingRecipe.IsDairyFree = updatedRecipe.IsDairyFree;
            existingRecipe.IsVegan = updatedRecipe.IsVegan;

            SaveRecipesToJson(_jsonFilePath);
        }

        public List<Recipe> GetAllRecipes()
        {
            _recipes = LoadRecipesFromJson(_jsonFilePath); 
            return new List<Recipe>(_recipes); 
        }

        public Recipe GetRecipeById(int recipeId)
        {
            _recipes = LoadRecipesFromJson(_jsonFilePath); 
            return _recipes.FirstOrDefault(r => r.Id == recipeId) ?? throw new Exception($"No recipe found with ID: {recipeId}");
        }

        public bool RemoveRecipe(int recipeId)
        {
            var recipe = _recipes.FirstOrDefault(x => x.Id == recipeId);
            if (recipe != null)
            {
                _recipes.Remove(recipe);
                SaveRecipesToJson(_jsonFilePath);
                return true;
            }
            return false;
        }

        public List<Recipe> SearchRecipe(string criteria)
        {

            _recipes = LoadRecipesFromJson(_jsonFilePath);

            if (string.IsNullOrWhiteSpace(criteria))
            {
                throw new ArgumentException("Search criteria cannot be null or empty.", nameof(criteria));
            }

            var searchedCriteria = _recipes.Where(r =>
                (r.Name?.Contains(criteria, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (r.Category?.Contains(criteria, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

            return searchedCriteria;

        }
    }
}
