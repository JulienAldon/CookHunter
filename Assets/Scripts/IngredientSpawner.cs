using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public string type;
    public GameObject ingredient;
    public float spawnTime;
    private float spawnTimer;
    public bool movingRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime) {
            GameObject a = Instantiate(ingredient, transform.position, Quaternion.identity);
            a.GetComponent<Ingredient>().type = type;
            a.GetComponent<Ingredient>().movingRight = movingRight;
            spawnTimer = 0;
        }
    }
}
