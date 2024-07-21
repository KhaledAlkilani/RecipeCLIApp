﻿using RecipeCLIApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCLIApp
{
    public class Recipes_Data
    {
        public static List<Recipe> Recipes = new List<Recipe>
    {
        new Recipe
        {
            Id = 3,
            Name = "Rice Porridge",
            Category = "Breakfast",
            Ingredients = new List<string> { "Rice", "Milk", "Cinnamon", "Honey" },
            Instructions = new List<string> { "Mix ingredients", "Boil", "Add cinnamon", "Serve" },
            IsGlutenFree = false,
            IsDairyFree = false,
            IsVegan = false
        },

        new Recipe
        {
            Id = 10,
            Name = "Chicken Caesar Salad",
            Category = "Salad",
            Ingredients = new List<string> { "Chicken breasts", "Romaine lettuce", "Caesar dressing", "Parmesan cheese", "Croutons" },
            Instructions = new List<string> { "Combine all ingredients", "Toss", "Serve immediately" },
            IsGlutenFree = false,
            IsDairyFree = false,
            IsVegan = false
        },

        new Recipe
        {
            Id = 4,
            Name = "Tasty Fish Soup",
            Category = "Soup",
            Ingredients = new List<string> {"Fish, water, potato, spicies, carrot."},
            Instructions = new List<string> {
              "Put water in saucepan, clean the fish, put fish in saucepan, cut the potato and carrot into small pieces, put potato and carrots into the saucepan. Put spicies also and mix all together and put the saucepan on the stove for 30min."
                    },
            IsGlutenFree = false,
            IsDairyFree = true,
            IsVegan = false
        },

        new Recipe
        {
            Id = 5,
            Name = "Classic Tomator Soup",
            Category = "Soup",
            Ingredients = new List<string> { "4 cups tomator juice, 14 tomatoes, peeled and chopped, 1/4 cup butter, 2 teaspoons salt, 2 teaspoons sugar." },
            Instructions = new List<string> {"\"Combine all ingredients in a large pot, Simmer for 30 minutes over medium heat, Puree with an immersion blender until smooth."},
            IsGlutenFree = true,
            IsDairyFree = false,
            IsVegan = false
        },

        new Recipe
        {
            Id = 6,
            Name = "Avocado Quinoa Salad",
            Category = "Salad",
            Ingredients = new List<string> {
              "1 cup quinoa\", 2 avocados diced, 1 cucumber diced, 1 tomato, diced, Juice of 1 lime, Salt and pepper to taste." },
            Instructions = new List<string> {
              "Cook quinoa according to package instructions and let cool. In a large bowl, combine all ingredients. Toss with lime juice, salt, and pepper before serving.",
              "Drink water."
            },
            IsGlutenFree = null,
            IsDairyFree = null,
            IsVegan = null
        },

        new Recipe
        {
            Id = 7,
            Name = "Vegan Chocolate Cake",
            Category = "Dessert",
            Ingredients = new List<string> {"1 cup unsweetened almond milk, 1 tablespoon apple cider vinegar, 2 cups all-purpose flour, 1 cup granulated sugar, 3/4 cup cocoa powder, 2 teaspoons baking powder, 1/2 cup vegetable oil, 1 teaspoon vanilla extract."},
            Instructions = new List<string> {"Preheat oven to 350°F (175°C). Mix almond milk and vinegar and set aside for a few minutes to curdle. Sift together flour, sugar, cocoa, and baking powder. Add oil and vanilla to almond milk mixture, then combine with dry ingredients. Pour into greased pan and bake for 35 minutes."},
            IsGlutenFree = false,
            IsDairyFree = true,
            IsVegan = true
        },

        new Recipe
        {
            Id = 8,
            Name = "Beef Stroganoff",
            Category = "Main Course",
            Ingredients = new List<string> {"1 pound beef sirloin sliced thin. 4 tablespoons butter. 1 onion chopped. 1 clove garlic minced. 1/2 pound mushrooms sliced. 1 cup beef broth. 1 cup sour cream. Salt and pepper to taste."},
            Instructions = new List<string> {"In a skillet, brown beef in 2 tablespoons butter and remove. In the same skillet, add remaining butter onion, and garlic. Add mushrooms and sauté until cooked. Return beef to skillet, add beef broth, and simmer for 10 minutes. Stir in sour cream and heat through without boiling. Season with salt and pepper."},
            IsGlutenFree = true,
            IsDairyFree = false,
            IsVegan = false
        },

          new Recipe
        {
            Id = 9,
            Name = "Lentil Soup",
            Category = "Soup",
            Ingredients = new List<string> {"1 tablespoon olive oil, 1 large onion, chopped, 2 garlic cloves, minced\", 1 tablespoon tomato paste, 2 carrots, diced, 2 stalks celery, diced, 1 cup dried lentils, 4 cups vegetable broth, 1/2 teaspoon salt, 1/4 teaspoon black pepper."},
            Instructions = new List<string> {"Heat olive oil in a large pot over medium heat. Add onion and garlic, and cook until onion is translucent. Stir in tomato paste, carrots, and celery, and cook for a few minutes. Add lentils and vegetable broth, bring to a boil, then reduce heat and simmer for 30 minutes. Season with salt and pepper before serving."},
            IsGlutenFree = true,
            IsDairyFree = true,
            IsVegan = true
  },
          new Recipe
        {
            Id = 10,
            Name = "Chicken Caesar Salad",
            Category = "Salad",
            Ingredients = new List<string> {"2 chicken breasts, grilled and sliced, 4 cups romaine lettuce chopped, 1/2 cup Caesar dressing, 1/4 cup grated Parmesan cheese, 1 cup croutons."},
            Instructions = new List<string> {"In a large bowl, combine lettuce, chicken, and croutons. Add Caesar dressing and toss to coat. Sprinkle with Parmesan cheese and serve immediately."},
            IsGlutenFree = false,
            IsDairyFree = false,
            IsVegan = false
         },

          new Recipe
        {
            Id = 11,
            Name = "Spinach and Mushroom Quiche",
            Category = "Breakfast",
            Ingredients = new List<string> {"1 pie crust. 1 tablespoon olive oil. 1 onion, diced. 2 cups fresh spinach. 1 cup mushrooms sliced. 4 eggs. 1 cup milk. 1/2 cup shredded cheese. Salt and pepper to taste."
            },
            Instructions = new List<string> {"Preheat oven to 375°F (190°C). Heat olive oil in a skillet over medium heat. Add onion and mushrooms, cook until soft. Add spinach and cook until wilted. In a bowl, whisk together eggs, milk, cheese, salt, and pepper. Place the vegetable mixture into pie crust, pour the egg mixture over it. Bake for 35-40 minutes or until set."},
            IsGlutenFree = false,
            IsDairyFree = false,
            IsVegan = false
        },

          new Recipe
        {
            Id = 12,
            Name = "Banana Nut Muffins",
            Category = "Dessert",
            Ingredients = new List<string> {"3 ripe bananas mashed, 1/3 cup melted butter, 3/4 cup sugar, 1 egg beaten, 1 teaspoon vanilla extract, 1 teaspoon baking soda. Pinch of salt, 1 1/2 cups of all-purpose flour, 1/2 cup walnut pieces."
            },
            Instructions = new List<string> {
              "Preheat oven to 350°F (175°C). Mix butter into the mashed bananas. Mix in the sugar, egg, and vanilla. Sprinkle the baking soda and salt over the mixture and mix in. Add the flour, mix until it is just incorporated. Fold in the chopped walnuts. Pour mixture into a prepared muffin tin. Bake for 25-30 minutes, or until a tester inserted into the center comes out clean."
            },
            IsGlutenFree = false,
            IsDairyFree = false,
            IsVegan = false
        }

    };
    };
}
