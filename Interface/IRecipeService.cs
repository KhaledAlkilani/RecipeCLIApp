using RecipeCLIApp.Model;

namespace RecipeCLIApp.Interface
{
    public interface IRecipeService
    {
        public void AddRecipe(Recipe recipe);
        public bool RemoveRecipe(int recipeId);
        public Recipe GetRecipeById(int recipeId);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> SearchRecipe(string criteria);


    }
}
