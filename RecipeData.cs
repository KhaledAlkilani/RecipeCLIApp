using RecipeCLIApp.Model;
using System.Collections.Generic;

public static class RecipeData
{
    public static List<Recipe> GetRecipes()
    {
        return new List<Recipe>
        {
            new Recipe
            {
                Id = 1,
                Name = "Sugar Water",
                Ingredients = new List<string> { "Water", "Sugar" },
                Instructions = new List<string> { "Mix", "Boil" }
            },
            new Recipe
            {
                Id = 2,
                Name = "Lemonade",
                Ingredients = new List<string> { "Water", "Lemon", "Sugar" },
                Instructions = new List<string> { "Mix ingredients", "Chill", "Serve" }
            }
        };
    }
};