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
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO: Move this elsewhere
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        // TODO: change how ingredients are instantiated
        Game.recipes.Add(new Recipe());
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Game.score.ToString();

        if (Game.recipes[0].isRecipeDone()) {
            Game.recipes.RemoveAt(0);
            Game.score += 1;
            Game.recipes.Add(new Recipe());
        }
        if (Input.GetMouseButtonDown(0)) {
            fire();
        }
    }

    void fire() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        int LayerIndex = LayerMask.NameToLayer("Ingredients");
        int layerMask = (1 << LayerIndex);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null) {
            Destroy(hit.collider.gameObject, 1f);
            hit.collider.attachedRigidbody.AddForce(1000 * Vector2.up);
            Game.recipes[0].validateIngredient(hit.collider.attachedRigidbody.GetComponent<Ingredient>().type);

        }
        fusilParticles.transform.position = fusil.position;
        fusilParticles.GetComponent<ParticleSystem>().Play();
        fusilAnim.SetTrigger("fire");
    }
}
