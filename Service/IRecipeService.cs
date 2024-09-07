using RecipeCLIApp.Model;

namespace RecipeCLIApp.Service
{
    public interface IRecipeService
    {
        public bool AddRecipe(Recipe recipe);
        public bool UpdateRecipe(int recipeId, Recipe updatedRecipe);
        public bool RemoveRecipe(int recipeId);
        public Recipe GetRecipeById(int recipeId);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> SearchRecipe(string criteria);


    }
}
