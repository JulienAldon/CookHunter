using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Transform fusil;
    public GameObject fusilParticles;
    public Animator fusilAnim;
    public Animator cursorAnim;
    public GameObject inHand;
    public GameObject cursor;
    public Animator camera;
    public SoundEffectMixer sound;

    public void LoadLevel(string level) {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        inHand = null;
        // TODO: Move this elsewhere
        // TODO: change how ingredients are instantiated
        // Game.recipes.Add(new Recipe());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            fire();
        } else if (Input.GetMouseButtonDown(1)) {
            take();
        }
    }

    void clearHand() {
        inHand.transform.parent = null;
        inHand = null;
    }

    void fire() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        int enemy = 1 << LayerMask.NameToLayer("Enemy");
        int table = 1 << LayerMask.NameToLayer("Table");
        int layerMask = interactable | enemy | table;
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, layerMask);
        if (inHand) { // interactable in hand
            if (hit.collider) { 
                if (hit.collider.gameObject.tag == "Ustencile" && inHand.tag == "Ingredient") {
                    hit.collider.gameObject.GetComponent<Ustencile>().AddIngredient(inHand.GetComponent<Ingredient>());
                    Destroy(inHand);
                    //TODO: Better destruction animation or fill animation (ustencile side)
                } else if (inHand.tag == "Ustencile" && hit.collider.gameObject.tag == "Ingredient") {
                    inHand.GetComponent<Ustencile>().AddIngredient(hit.collider.gameObject.GetComponent<Ingredient>());
                    Destroy(hit.collider.gameObject);
                    //TODO: Better destruction animation or fill animation (ustencile side)
                } else if (inHand.tag == "Ustencile" && hit.collider.gameObject.tag == "Table") {
                    if (!hit.collider.gameObject.GetComponent<Table>().satisfied) {
                        if (hit.collider.gameObject.GetComponent<Table>().ValidateRecipe(inHand.GetComponent<Ustencile>().ingredients) == true) {
                            // TODO: JUICE cette action doit etre marquante : tremblement de camera + particle system + bruit de succes
                            // Success
                            // + Score or + Request Good
                        } else {
                            // TODO: JUICE tremblement de camera + bruit d'erreur
                            // Fail
                            // - Score or -Request
                        }
                        inHand.GetComponent<Ustencile>().RemoveIngredients();
                    } else {
                        Debug.Log("already satisfied");
                    }
                    // Validate the content -> validate a recipe
                } else {
                    Debug.Log("aze");
                }
            }
            else {
                // place back the interactable
                inHand.transform.position = new Vector3(mousePos.x, mousePos.y, inHand.transform.position.z);
                inHand.layer = LayerMask.NameToLayer("Interactable");
                clearHand();
            }
            ShootEffect();
            return;
        }
        if (hit.collider) {
            camera.SetTrigger("Shake");
            if (hit.collider.gameObject.tag == "Enemy") {
                hit.collider.gameObject.GetComponent<Enemy>().Death();
            }
            if (hit.collider.gameObject.tag == "Ingredient") {
                hit.collider.gameObject.GetComponent<Ingredient>().Death();

            }
            if (hit.collider.gameObject.tag == "Seed") {
                hit.collider.gameObject.GetComponent<Seed>().Death();

            }
        }
        ShootEffect();
    }

    void ShootEffect()
    {
        fusilParticles.transform.position = fusil.position;
        Instantiate(fusilParticles);
        fusilAnim.SetTrigger("fire");
        cursorAnim.SetTrigger("shoot");
        sound.MakeShotSound();
    }

    void take() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        int LayerIndex = LayerMask.NameToLayer("Interactable");
        int layerMask = (1 << LayerIndex);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, layerMask);
        
        if (inHand) {
            return;
        } if (hit.collider != null) {
            inHand = hit.collider.gameObject;
            inHand.transform.parent = cursor.transform;
            inHand.layer = LayerMask.NameToLayer("Uninteractable");
        }
        cursorAnim.SetTrigger("take");
        sound.MakeGrabSound();
    }
}
