using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game 
{
    public static List<Recipe> recipes = new List<Recipe>();
    public static string[] possibleIngredients = { "potato", "sausage", "tomato", "dough" };
    public static int score = 0;
}

public class Recipe
{
    public List<string> ingredients = new List<string>();
    public string[] done = new string[4];
    private List<string> todo = new List<string>();

    public Recipe() {
        int nb = 4;
        
        for (int i = 0; i < nb; i++) {
            ingredients.Add(Game.possibleIngredients[Random.Range(0, Game.possibleIngredients.Length)]);
        }
        done = new string[4];
        todo = new List<string>(ingredients);
    }

    public bool isIngredientValid(string type)
    {
        if (todo.Contains(type))
            return true;
        return false;
    }

    public void validateIngredient(string type)
    {
        if (isIngredientValid(type)) {
            Debug.Log(todo.IndexOf(type));
            done[todo.IndexOf(type)] = type;
            todo[todo.IndexOf(type)] = null;
        } else {
            done = new string[4]; //reset recipe progression or destroy recipe
            todo = new List<string>(ingredients);
        }
    }

    public bool isRecipeDone() 
    {
        foreach (var elem in todo) {
            if (elem != null) {
                return false;
            }
        }
        return true;
    }
}
