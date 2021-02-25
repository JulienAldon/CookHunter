using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public enum ingredientTypes {
    Tomato, Leek
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
    public float allyPenality = 10;
    public GameObject canvas;
    private SoundEffectMixer sound;
    public GameObject explodeParticles;
    public Color32 ingredientColor;
    
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffectMixer>();
        kitchen = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<Tilemap>();
        grid =  GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Uninteractable")) {
            timer = 0;
            canvas.SetActive(false);
            return;
        }
        canvas.SetActive(true);
        timer += Time.deltaTime;
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        int uninteractable = 1 << LayerMask.NameToLayer("Uninteractable");
        int layermask = interactable | uninteractable;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, .2f, layermask);
        if (hit.Length <= 1) {
            SpawnSeedOrEnemy();
            
        } else if (hit[1].GetComponent<BoxCollider2D>().tag != "Ingredient") {
            timer = 0;
            canvas.SetActive(false);
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
        sound.MakeSprotchSound();
        var a = Instantiate(explodeParticles, transform.position, Quaternion.identity);
        a.GetComponent<ParticleSystem>().startColor = ingredientColor;
        Destroy(gameObject);
    }
}
