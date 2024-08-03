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

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Provided recipe is null.");
            }

            try
            {
               _recipeRepository.AddRecipe(recipe);
           
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                Console.WriteLine("A recipe with the same name and category already exists.");
            }
        }

        public void UpdateRecipe(int recipeId, Recipe updatedRecipe)
        {
            if (updatedRecipe == null)
            {
                throw new ArgumentNullException(nameof(updatedRecipe), "Updated recipe is null.");
            }

            var existingRecipe = _recipeRepository.GetRecipeById(recipeId);
            if (existingRecipe == null)
            {
                throw new Exception("Recipe not found.");
            }

            updatedRecipe.Id = recipeId;
            _recipeRepository.UpdateRecipe(updatedRecipe);
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
