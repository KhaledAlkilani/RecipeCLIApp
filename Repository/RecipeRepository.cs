﻿using Npgsql;
using RecipeCLIApp.Model;
using RecipeCLIApp.Repository;
using System;

namespace RecipeCLIApp.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;

        public RecipeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool EnsureRecipesTableExists()
        {
           return ExecuteNonQuery(cmd =>
            {
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS recipes (
                        id SERIAL PRIMARY KEY,
                        name VARCHAR(255) NOT NULL,
                        category VARCHAR(255) NOT NULL,
                        ingredients TEXT[] NOT NULL,
                        instructions TEXT[] NOT NULL,
                        is_gluten_free BOOLEAN,
                        is_dairy_free BOOLEAN,
                        is_vegan BOOLEAN,
                        CONSTRAINT unique_name_category UNIQUE (name, category)
                    )";
                cmd.ExecuteNonQuery();
            });
        }

        private bool ExecuteNonQuery(Action<NpgsqlCommand> commandAction)
        {
            try
            {

                using (var conn = new NpgsqlConnection(_connectionString))

                {

                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        commandAction(cmd);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private T ExecuteQuery<T>(Func<NpgsqlCommand, T> queryFunc)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    return queryFunc(cmd);
                }
            }
        }

        public bool AddRecipe(Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));

            try
            {
                int insertedId = ExecuteQuery(cmd =>
                {
                    cmd.CommandText = @"
                        INSERT INTO recipes (name, category, ingredients, instructions, is_gluten_free, is_dairy_free, is_vegan) 
                        VALUES (@name, @category, @ingredients, @instructions, @is_gluten_free, @is_dairy_free, @is_vegan) 
                        RETURNING id";

                    cmd.Parameters.AddWithValue("name", recipe.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("category", recipe.Category ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ingredients", recipe.Ingredients != null ? (object)recipe.Ingredients.ToArray() : DBNull.Value);
                    cmd.Parameters.AddWithValue("instructions", recipe.Instructions != null ? (object)recipe.Instructions.ToArray() : DBNull.Value);
                    cmd.Parameters.AddWithValue("is_gluten_free", recipe.IsGlutenFree.HasValue ? (object)recipe.IsGlutenFree.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("is_dairy_free", recipe.IsDairyFree.HasValue ? (object)recipe.IsDairyFree : DBNull.Value);
                    cmd.Parameters.AddWithValue("is_vegan", recipe.IsVegan.HasValue ? (object)recipe.IsVegan : DBNull.Value);

                    object result = cmd.ExecuteScalar() ?? throw new Exception();
                    return result != null && result != DBNull.Value ? (int)result : 0;
                });

                if (insertedId > 0)
                {
                    Console.WriteLine($"Recipe added successfully with ID: {insertedId}");
                    return true;

                }
                else

                {
                    Console.WriteLine("Failed to add recipe");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the recipe: {ex.Message}");
                return false;
            }

        }

        public bool UpdateRecipe(Recipe updatedRecipe)
        {
            try
            {
                ExecuteNonQuery(cmd =>
            {
                cmd.CommandText = @"
            UPDATE recipes
            SET name = @name, category = @category, ingredients = @ingredients, instructions = @instructions,
                is_gluten_free = @is_gluten_free, is_dairy_free = @is_dairy_free, is_vegan = @is_vegan
            WHERE id = @id";

                cmd.Parameters.AddWithValue("id", updatedRecipe.Id);
                cmd.Parameters.AddWithValue("name", updatedRecipe.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("category", updatedRecipe.Category ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("ingredients", updatedRecipe.Ingredients != null ? (object)updatedRecipe.Ingredients.ToArray() : DBNull.Value);
                cmd.Parameters.AddWithValue("instructions", updatedRecipe.Instructions != null ? (object)updatedRecipe.Instructions.ToArray() : DBNull.Value);
                cmd.Parameters.AddWithValue("is_gluten_free", updatedRecipe.IsGlutenFree.HasValue ? (object)updatedRecipe.IsGlutenFree.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("is_dairy_free", updatedRecipe.IsDairyFree.HasValue ? (object)updatedRecipe.IsDairyFree.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("is_vegan", updatedRecipe.IsVegan.HasValue ? (object)updatedRecipe.IsVegan.Value : DBNull.Value);

                cmd.ExecuteNonQuery();
            });
                return true;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error updating recipe: {ex.Message}");
                return false;
            }
        }

        public bool DeleteRecipeById(int id)
        {
            try
            {
                ExecuteNonQuery(cmd =>
                {
                    cmd.CommandText = "DELETE FROM recipes WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                });

                return true;

            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting recipe with ID {id}: {ex.Message}");
                return false;
            }
          
        }

        public Recipe? GetRecipeById(int id)
        {
            return ExecuteQuery(cmd =>
            {
                cmd.CommandText = "SELECT * FROM recipes WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Recipe
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? null : reader.GetString(reader.GetOrdinal("category")),
                            Ingredients = reader.IsDBNull(reader.GetOrdinal("ingredients")) ? null : reader.GetFieldValue<string[]>(reader.GetOrdinal("ingredients")).ToList(),
                            Instructions = reader.IsDBNull(reader.GetOrdinal("instructions")) ? null : reader.GetFieldValue<string[]>(reader.GetOrdinal("instructions")).ToList(),
                            IsGlutenFree = reader.IsDBNull(reader.GetOrdinal("is_gluten_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_gluten_free")),
                            IsDairyFree = reader.IsDBNull(reader.GetOrdinal("is_dairy_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_dairy_free")),
                            IsVegan = reader.IsDBNull(reader.GetOrdinal("is_vegan")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_vegan"))
                        };
                    }

                    return null;
                }
            });
        }

        public List<Recipe> GetAllRecipes()
        {
            return ExecuteQuery(cmd =>
            {
                cmd.CommandText = "SELECT * FROM recipes";

                using (var reader = cmd.ExecuteReader())
                {
                    var recipes = new List<Recipe>();
                    while (reader.Read())
                    {
                        recipes.Add(new Recipe
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Category = reader.GetString(reader.GetOrdinal("category")),
                            Ingredients = reader.GetFieldValue<string[]>(reader.GetOrdinal("ingredients")).ToList(),
                            Instructions = reader.GetFieldValue<string[]>(reader.GetOrdinal("instructions")).ToList(),
                            IsGlutenFree = reader.IsDBNull(reader.GetOrdinal("is_gluten_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_gluten_free")),
                            IsDairyFree = reader.IsDBNull(reader.GetOrdinal("is_dairy_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_dairy_free")),
                            IsVegan = reader.IsDBNull(reader.GetOrdinal("is_vegan")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_vegan"))
                        });
                    }

                    return recipes;
                }
            });
        }

        public List<Recipe> SearchRecipes(string criteria)
        {
            return ExecuteQuery(cmd =>
            {
                cmd.CommandText = @"
                    SELECT * FROM recipes
                    WHERE name ILIKE @criteria OR category ILIKE @criteria";
                cmd.Parameters.AddWithValue("criteria", $"%{criteria}%");

                using (var reader = cmd.ExecuteReader())
                {
                    var recipes = new List<Recipe>();
                    while (reader.Read())
                    {
                        recipes.Add(new Recipe
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Category = reader.GetString(reader.GetOrdinal("category")),
                            Ingredients = reader.GetFieldValue<string[]>(reader.GetOrdinal("ingredients")).ToList(),
                            Instructions = reader.GetFieldValue<string[]>(reader.GetOrdinal("instructions")).ToList(),
                            IsGlutenFree = reader.IsDBNull(reader.GetOrdinal("is_gluten_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_gluten_free")),
                            IsDairyFree = reader.IsDBNull(reader.GetOrdinal("is_dairy_free")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_dairy_free")),
                            IsVegan = reader.IsDBNull(reader.GetOrdinal("is_vegan")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("is_vegan"))
                        });
                    }

                    return recipes;
                }
            });
        }

        public bool RecipeExists(string name)
        {
            return GetAllRecipes()
                .Any(r => (r.Name ?? string.Empty).Equals(name, StringComparison.OrdinalIgnoreCase));
        }


    }
}
