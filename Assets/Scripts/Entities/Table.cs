using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public String name;
    public List<ingredientTypes> ingredients;
}
    
[System.Serializable]
public class RecipeList
{
    public List<Recipe> recipes;
}

public class Table : MonoBehaviour
{
    public Sprite tomatoSoupRecipe;
    public Sprite soupRecipe;
    public GameObject recipeIngredients;
    public bool satisfied = false;
    public Sprite fullSprite;
    public Sprite emptySprite;
    public RecipeList possibleRequest;
    public Recipe request;
    public Animator validate;
    public SoundEffectMixer sound;
    // Start is called before the first frame update
    void Start()
    {
        request = possibleRequest.recipes[UnityEngine.Random.Range(0, possibleRequest.recipes.Count)];

        if (request.name == "Soup") {
            recipeIngredients.GetComponent<SpriteRenderer>().sprite = soupRecipe;
        } else if (request.name == "TomatoSoup") {
            recipeIngredients.GetComponent<SpriteRenderer>().sprite = tomatoSoupRecipe;
        }

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffectMixer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ValidateRecipe(List<ingredientTypes> ingr)
    {
        bool isEqual = ScrambledEquals<ingredientTypes>(ingr, request.ingredients);
        if (isEqual) {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
            validate.SetTrigger("validate");
            sound.MakeVictorySound();
            recipeIngredients.SetActive(false);
            satisfied = true;
        } else {
            validate.SetTrigger("fail");
            sound.MakeDefaitSound();
        }
        return isEqual;
    }

    public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2) {
        var cnt = new Dictionary<T, int>();
        foreach (T s in list1) {
            if (cnt.ContainsKey(s)) {
            cnt[s]++;
            } else {
            cnt.Add(s, 1);
            }
        }
        foreach (T s in list2) {
            if (cnt.ContainsKey(s)) {
            cnt[s]--;
            } else {
            return false;
            }
        }
        return cnt.Values.All(c => c == 0);
    }
}
