//using Newtonsoft.Json;
//using RecipeCLIApp.Model;
//using System.Text;

//class RecipeApiClient
//{
//    private readonly HttpClient _client = new HttpClient();
//    private readonly string _apiKey = "15b0375d06d34c8a80f10569b3aff329";

//    public async Task<string> GetRecipeAsync(string query)
//    {
//        try
//        {
//            string url = $"https://api.spoonacular.com/recipes/complexSearch?apiKey={_apiKey}&query={query}";
//            HttpResponseMessage response = await _client.GetAsync(url);
//            response.EnsureSuccessStatusCode();
//            string responseBody = await response.Content.ReadAsStringAsync();
//            return responseBody;
//        }
//        catch (HttpRequestException e)
//        {
//            Console.WriteLine("\nException Caught!");
//            Console.WriteLine("Message :{0} ", e.Message);
//            return null;
//        }
//    }

//    public async Task<Recipe> GetRecipeDetailsAsync(int recipeId)
//    {
//        try
//        {
//            string url = $"https://api.spoonacular.com/recipes/{recipeId}/information?apiKey={_apiKey}&includeNutrition=false";
//            HttpResponseMessage response = await _client.GetAsync(url);
//            response.EnsureSuccessStatusCode();
//            string responseBody = await response.Content.ReadAsStringAsync();
//            var detailedRecipe = JsonConvert.DeserializeObject<SpoonacularDetailedRecipe>(responseBody);
//            return new Recipe
//            {
//                Id = detailedRecipe.Id,
//                Name = detailedRecipe.Title,
//                IsGlutenFree = detailedRecipe.GlutenFree,
//                IsDairyFree = detailedRecipe.DairyFree,
//                IsVegan = detailedRecipe.Vegan,
//                Ingredients = detailedRecipe.Ingredients.Select(i => i.Name).ToList(),
//                Instructions = detailedRecipe.Instructions.Select(ins => ins.Step).ToList()
//            };
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("\nException Caught!");
//            Console.WriteLine("Message :{0} ", e.Message);
//            return null;
//        }
//    }

//    public async Task<Recipe> AddRecipeAsync(Recipe recipe)
//    {
//        string url = $"https://api.spoonacular.com/recipes?apiKey={_apiKey}";
//        string json = JsonConvert.SerializeObject(recipe);
//        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

//        HttpResponseMessage response = await _client.PostAsync(url, content);
//        response.EnsureSuccessStatusCode();
//        string responseBody = await response.Content.ReadAsStringAsync();

//        var updatedRecipe = JsonConvert.DeserializeObject<Recipe>(responseBody);
//        return updatedRecipe ?? recipe;
//    }

//    public async Task<List<Recipe>> SearchRecipesAsync(string query)
//    {
//        string url = $"https://api.spoonacular.com/recipes/complexSearch?apiKey={_apiKey}&query={query}";
//        HttpResponseMessage response = await _client.GetAsync(url);
//        response.EnsureSuccessStatusCode();
//        string responseBody = await response.Content.ReadAsStringAsync();

//        var searchResults = JsonConvert.DeserializeObject<SpoonacularSearchResult>(responseBody);
//        if (searchResults == null || searchResults.Results == null)
//        {
//            return new List<Recipe>();
//        }

//        List<Recipe> recipes = new List<Recipe>();
//        foreach (var summary in searchResults.Results)
//        {
//            Recipe recipe = await GetRecipeDetailsAsync(summary.Id);
//            if (recipe != null)
//            {
//                recipes.Add(recipe);
//            }
//        }

//        return recipes;
//    }

//    public async Task RemoveRecipeAsync(int recipeId)
//    {
//        string url = $"https://api.spoonacular.com/recipes/{recipeId}?apiKey={_apiKey}";
//        HttpResponseMessage response = await _client.DeleteAsync(url);
//        response.EnsureSuccessStatusCode();
//    }

//}

