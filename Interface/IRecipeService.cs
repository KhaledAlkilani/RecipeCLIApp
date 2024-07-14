using RecipeCLIApp.Model;

namespace RecipeCLIApp.Interface
{
    public interface IRecipeService
    {
        public void AddRecipe(Recipe recipe);
        public void UpdateRecipe(int recipeId, Recipe updatedRecipe);
        public bool RemoveRecipe(int recipeId);
        public Recipe GetRecipeById(int recipeId);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> SearchRecipe(string criteria);


    }
}
