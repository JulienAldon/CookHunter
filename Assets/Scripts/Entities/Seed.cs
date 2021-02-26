using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public Slider slider;
    public ingredientTypes type;
    public float timeToSpawnAnIngredient;
    private float timer;
    public GameObject Ingredient;
    public GameObject canvas;
    private SoundEffectMixer sound;
    public GameObject explodeParticles;
    public Color32 ingredientColor;
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffectMixer>();
        timer = 0;
    }

    bool IngredientNear() {
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        int uninteractable = 1 << LayerMask.NameToLayer("Uninteractable");
        int layermask = interactable | uninteractable;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, .1f, layermask);
        if (hit.Length <= 1) {
            return false;
        } else {
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Uninteractable") && IngredientNear()) {
            timer = 0;
            canvas.SetActive(false);
            return;
        }
        canvas.SetActive(true);
        timer += Time.deltaTime;
        
        if (!IngredientNear() && transform.parent == null) {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            if (timer >= timeToSpawnAnIngredient) {
                SpawnIngredient();
                timer = 0;
            }
        } else {
            gameObject.layer = LayerMask.NameToLayer("Uninteractable");
        }

        slider.value = timer / timeToSpawnAnIngredient;
    }

    void SpawnIngredient()
    {
        var a = Instantiate(Ingredient, transform.position, Quaternion.identity);
    }

    public void Death() {
        // Explodey animation
        Destroy(gameObject);
        var a = Instantiate(explodeParticles, transform.position, Quaternion.identity);
        a.GetComponent<ParticleSystem>().startColor = ingredientColor;
        sound.MakeSprotchSound();
    }
}
