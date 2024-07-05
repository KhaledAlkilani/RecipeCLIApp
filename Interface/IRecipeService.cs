using RecipeCLIApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCLIApp.Interface
{
    public interface IRecipeService
    {
        public void AddRecipe(Recipe recipe);
        public void RemoveRecipe(int recipeId);
        public Recipe GetRecipeById(int recipeId);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> searchRecipe(string criteria);


    }
}
