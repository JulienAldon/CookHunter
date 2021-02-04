using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    float timer = 0;
    public float recipeSpawnTime;
    public GameObject ingredient;
    public TextMeshProUGUI score;
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO: change how ingredients are instantiated
        SpawnIngredient();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Game.score.ToString();
        timer += Time.deltaTime;
        if (timer >= recipeSpawnTime) {
            timer = 0;
            SpawnIngredient();
        }
    }

    void SpawnIngredient() {
        Game.recipes.Add(new Recipe());
        foreach (var elem in Game.recipes[0].ingredients) {
            var ing = Instantiate(ingredient, new Vector2(Random.Range(-8, 8),Random.Range(5, -5)), Quaternion.identity);
            ing.GetComponent<Ingredient>().type = elem;
        }
    }
}
