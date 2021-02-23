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
    
    void Start()
    {
        kitchen = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<Tilemap>();
        grid =  GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
            return;
        timer += Time.deltaTime;
        slider.value = timer / TimeBeforeTransformation;
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, .2f, interactable);
        if (hit.Length <= 1) {
            if (timer >= TimeBeforeTransformation) {
                SpawnSeedOrEnemy();
                timer = 0;
            }
        } else {
            timer = 0;
        }

    }

    void SpawnSeedOrEnemy()
    {
         Vector3Int target = grid.WorldToCell(transform.position);
        Debug.Log(kitchen.HasTile(target));
        Debug.Log(transform.position);
        Debug.Log(grid.WorldToCell(transform.position));
        if (kitchen.HasTile(target)) { // si l'ingredient est sur le sol de la cuisine

            // Instantiate Seed si pas de seed
            Instantiate(seed, transform.position, Quaternion.identity);
        } else { // si l'ingredient est dans le wilderness
           // Instantiate Enemy si pas d'ennemy
           Instantiate(enemy, transform.position, Quaternion.identity);
        }
        // Animation de mort
        Destroy(gameObject);
    }

    public void Death() {
        // Explodey animation
        Destroy(gameObject);
    }
}
