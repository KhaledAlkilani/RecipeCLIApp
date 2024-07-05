
namespace RecipeCLIApp.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public List<string>? Ingredients { get; set; }
        public List<string>? Instructions { get; set; }
        public bool? IsGlutenFree { get; set; }
        public bool? IsDairyFree { get; set; }
        public bool? IsVegan {  get; set; }
    }
}
