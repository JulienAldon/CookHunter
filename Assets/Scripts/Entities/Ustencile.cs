using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UstencilesTypes {
    Marmite, Saladier
}

public class Ustencile : MonoBehaviour
{
    public int maxIngredient;
    public UstencilesTypes type;
    public GameObject[] ingredientInside;
    public List<Ingredient> ingredients = new List<Ingredient>();
    public Sprite fullSprite;
    public Sprite emptySprite;
    public Sprite midSprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ingredients.Count >= maxIngredient) {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
        } else if (ingredients.Count == 0) {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = midSprite;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
            return;
    }

    public void AddIngredient(Ingredient ingr)
    {
        if (ingredients.Count < maxIngredient) {
            ingredients.Add(ingr);
            Debug.Log(ingredients.Count);
            ingredientInside[ingredients.Count - 1].SetActive(true);
            ingredientInside[ingredients.Count - 1].GetComponent<SpriteRenderer>().sprite = ingr.GetComponent<SpriteRenderer>().sprite;
        } else {
            // maximum count ingredient in ustencile
        }
    }

    public void RemoveIngredients()
    {
        ingredients = new List<Ingredient>();
        foreach (var elem in ingredientInside) {
            elem.SetActive(false);
        }

        // Sound & particles effects
    }

    public void RemoveLastIngredient()
    {
        ingredients.RemoveAt(ingredients.Count - 1);
        //spawn particles showing you destroyes an ingredient
    }
}
