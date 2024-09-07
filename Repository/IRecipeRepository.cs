using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Npgsql;
using RecipeCLIApp.Model;

namespace RecipeCLIApp.Repository
{
    public interface IRecipeRepository
    {
        public void EnsureRecipesTableExists();
        public int AddRecipe(Recipe recipe);
        public void UpdateRecipe(Recipe recipe);
        public void DeleteRecipeById(int id);
        public Recipe? GetRecipeById(int id);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> SearchRecipes(string criteria);
        public bool RecipeExists(string name);
    }
}
