using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ustencilesTypes {
    Marmite, Saladier
}

public class Ustencile : MonoBehaviour
{
    public int maxIngredient;
    public GameObject[] ingredientInside;
    public List<Ingredient> ingredient = new List<Ingredient>();
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
        if (ingredient.Count >= maxIngredient) {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
        } else if (ingredient.Count == 0) {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = midSprite;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
            return;
    }

    public void AddIngredient(Ingredient ingr)
    {
        if (ingredient.Count < maxIngredient) {
            ingredient.Add(ingr);
            Debug.Log(ingredient.Count);
            ingredientInside[ingredient.Count - 1].SetActive(true);
            ingredientInside[ingredient.Count - 1].GetComponent<SpriteRenderer>().sprite = ingr.GetComponent<SpriteRenderer>().sprite;
        } else {
            // maximum count ingredient in ustencile
        }
    }
}
