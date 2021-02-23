using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public enum ingredientTypes {
    Tomato
}

public class Ingredient : MonoBehaviour
{
    private Tilemap kitchen;
    private Grid grid;
    public ingredientTypes type;
    public GameObject seed;
    public GameObject enemy;
    public float TimeBeforeTransformation;
    private float timer;
    public Slider slider;
    public float allyPenality;
    
    void Start()
    {
        kitchen = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<Tilemap>();
        grid =  GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Default")) {
            timer = 0;
            return;
        }
        timer += Time.deltaTime;
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, .2f, interactable);
        if (hit.Length <= 1) {
            SpawnSeedOrEnemy();
            
        } else {
            timer = 0;
        }
        if (kitchen.HasTile(grid.WorldToCell(transform.position))) {
            slider.value = timer / (TimeBeforeTransformation + allyPenality);
        } else {
            slider.value = timer / TimeBeforeTransformation;
        }
    }

    void SpawnSeedOrEnemy()
    {
        if (kitchen.HasTile(grid.WorldToCell(transform.position))) { // si l'ingredient est sur le sol de la cuisine
            // Instantiate Seed si pas de seed
            slider.value = timer / (TimeBeforeTransformation + allyPenality);
            if (timer >= TimeBeforeTransformation + allyPenality) {
                Instantiate(seed, transform.position, Quaternion.identity);
                Death();
                timer = 0;
            }
        } else { // si l'ingredient est dans le wilderness
           // Instantiate Enemy si pas d'ennemy
           slider.value = timer / TimeBeforeTransformation;
           if (timer >= TimeBeforeTransformation) {
                Instantiate(enemy, transform.position, Quaternion.identity);
                Death();
                timer = 0;
            }
        }
    }

    public void Death() {
        // Explodey animation
        Destroy(gameObject);
    }
}
