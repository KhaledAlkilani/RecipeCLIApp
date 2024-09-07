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
        public bool EnsureRecipesTableExists();
        public bool AddRecipe(Recipe recipe);
        public bool UpdateRecipe(Recipe recipe);
        public bool DeleteRecipeById(int id);
        public Recipe? GetRecipeById(int id);
        public List<Recipe> GetAllRecipes();
        public List<Recipe> SearchRecipes(string criteria);
        public bool RecipeExists(string name);
    }
}
