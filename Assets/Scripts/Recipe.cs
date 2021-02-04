using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game 
{
    public static List<Recipe> recipes = new List<Recipe>();
    public static string[] possibleIngredients = { "potato", "sausage", "tomato", "salad", "dough" };
    public static int score = 0;
}

public class Recipe
{
    public List<string> ingredients = new List<string>();
    public List<string> done = new List<string>();

    public Recipe() {
        int nb = Random.Range(3, 4);
        
        for (int i = 0; i <= nb; i++) {
            ingredients.Add(Game.possibleIngredients[Random.Range(0, Game.possibleIngredients.Length)]);
        }
    }

    public bool isIngredientValid(string type)
    {
        if (ingredients.Contains(type))
            return true;
        return false;
    }

    public void validateIngredient(string type)
    {
        if (isIngredientValid(type)) {
            done.Add(type);
            
        } else {
            done = new List<string>(); //reset recipe progression or destroy recipe
        }
    }

    public bool isRecipeDone() 
    {
        if (done.Count >= ingredients.Count) {
            return true;
        }
        return false;
    }
}
