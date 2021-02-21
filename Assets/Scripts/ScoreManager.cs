using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject ingredient;
    public TextMeshProUGUI score;
    public Transform fusil;
    public GameObject fusilParticles;
    public Animator fusilAnim;
    public Animator cursorAnim;
    public GameObject inHand;
    public GameObject cursor;
    
    // Start is called before the first frame update
    void Start()
    {
        inHand = null;
        // TODO: Move this elsewhere
        SceneManager.LoadScene("isometric_prototype", LoadSceneMode.Additive);
        // TODO: change how ingredients are instantiated
        // Game.recipes.Add(new Recipe());
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Game.score.ToString();

        // if (Game.recipes[0].isRecipeDone()) {
        //     Game.recipes.RemoveAt(0);
        //     Game.score += 1;
        //     Game.recipes.Add(new Recipe());
        // }
        if (Input.GetMouseButtonDown(0)) {
            fire();
        } else if (Input.GetMouseButtonDown(1)) {
            take();
        }
    }

    void fire() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        int interactable = 1 << LayerMask.NameToLayer("Interactable");
        int enemy = 1 << LayerMask.NameToLayer("Enemy");
        int layerMask = interactable | enemy;
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, layerMask);
        
        if (inHand) { // interactable in hand
            if (hit.collider) { 
                if (hit.collider.gameObject.tag == "Ustencile" && inHand.tag == "Ingredient") {
                    hit.collider.gameObject.GetComponent<Ustencile>().AddIngredient(inHand.GetComponent<Ingredient>().type);
                    Destroy(inHand); //TODO: Better destruction animation or fill animation (ustencile side)
                } else if (inHand.tag == "Ustencile" && hit.collider.gameObject.tag == "Ingredient") {
                    inHand.GetComponent<Ustencile>().AddIngredient(hit.collider.gameObject.GetComponent<Ingredient>().type);
                    Destroy(hit.collider.gameObject); //TODO: Better destruction animation or fill animation (ustencile side)
                }
            }
            else {
                // place back the interactable
                inHand.transform.parent = null;
                inHand.transform.position = mousePos;
                inHand.layer = LayerMask.NameToLayer("Interactable");
                inHand = null;
            }
            return;
        }
        if (hit.collider) {
            Debug.Log(hit.collider);
            if (hit.collider.gameObject.tag == "Enemy") {
                hit.collider.gameObject.GetComponent<Enemy>().Death();
            }
        }
        fusilParticles.transform.position = fusil.position;
        Instantiate(fusilParticles);
        fusilAnim.SetTrigger("fire");
        cursorAnim.SetTrigger("shoot");
    }

    void take() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (inHand) {
            return;
        }
        int LayerIndex = LayerMask.NameToLayer("Interactable");
        int layerMask = (1 << LayerIndex);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null) {
            inHand = hit.collider.gameObject;
            inHand.transform.parent = cursor.transform;
            inHand.layer = LayerMask.NameToLayer("Default");
        }
        cursorAnim.SetTrigger("take");
    }
}
