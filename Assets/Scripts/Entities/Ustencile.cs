using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ustencilesTypes {
    Marmite, Saladier
}

public class Ustencile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(ingredientTypes ingredient)
    {
        Debug.Log(ingredient);
    }
}
