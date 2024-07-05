using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCLIApp.Model
{
    public class SpoonacularSearchResult
    {
        public List<RecipeSummary> Results { get; set; }
    }

    public class RecipeSummary
    {
        public int Id { get; set; }
    }

    public class SpoonacularDetailedRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool GlutenFree { get; set; }
        public bool DairyFree { get; set; }
        public bool Vegan { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
    }

    public class Instruction
    {
        public string Step { get; set; }
    }

}
