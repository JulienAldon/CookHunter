using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public Slider slider;
    public float timeToSpawnAnIngredient;
    private float timer;
    public GameObject Ingredient;
    private bool ingredientNear;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
            return;
        timer += Time.deltaTime;
        slider.value = timer / timeToSpawnAnIngredient;
        if (timer >= timeToSpawnAnIngredient) {
            SpawnIngredient();
            timer = 0;
        }
        if (ingredientNear) {
            timer = 0;
            canvas.SetActive(false);
        } else {
            canvas.SetActive(true);
        }
    }

    void SpawnIngredient()
    {
        var a = Instantiate(Ingredient, transform.position, Quaternion.identity);
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ingredient") {
            ingredientNear = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ingredient") {
            ingredientNear = true;
        }
    }

    public void Death() {
        // Explodey animation
        Destroy(gameObject);
    }
}
