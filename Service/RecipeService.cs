using RecipeCLIApp.Model;
using Newtonsoft.Json;
using Npgsql;
using RecipeCLIApp.Repositories;
using RecipeCLIApp.Repository;

namespace RecipeCLIApp.Service
{
    public class RecipeService : IRecipeService
    {

        private List<Recipe> _recipes;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _recipeRepository.EnsureRecipesTableExists();
        }

        public bool AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                return false;
            }

            var existingRecipe = _recipeRepository.RecipeExists(recipe.Name ?? "UNKNOWN");

            if (existingRecipe)
            {
                Console.WriteLine("A recipe with the same name already exist.");
                return false;
            }

            _recipeRepository.AddRecipe(recipe);
            return true;
        }

        public bool UpdateRecipe(int recipeId, Recipe updatedRecipe)
        {
            if (updatedRecipe == null)
            {
                return false;
            }

            var existingRecipe = _recipeRepository.GetRecipeById(recipeId);
            if (existingRecipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return false;
            }

            updatedRecipe.Id = recipeId;
            _recipeRepository.UpdateRecipe(updatedRecipe);
            return true;
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAllRecipes();
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _recipeRepository.GetRecipeById(recipeId) ?? throw new Exception("Recipe not found.");
        }

        public bool RemoveRecipe(int recipeId)
        {
           var existingRecipe = _recipeRepository.GetRecipeById(recipeId);
            if (existingRecipe == null)
            {
                return false;
            }

            _recipeRepository.DeleteRecipeById(recipeId);
            return true;
        }

        public List<Recipe> SearchRecipe(string criteria)
        {

            if (string.IsNullOrWhiteSpace(criteria))
            {
                throw new ArgumentException("Search criteria cannot be null or empty.", nameof(criteria));
            }

            return _recipeRepository.SearchRecipes(criteria);

        }
    }
}
