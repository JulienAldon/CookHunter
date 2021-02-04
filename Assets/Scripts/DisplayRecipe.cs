using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRecipe : MonoBehaviour
{
    public Image[] ingredients;
    public Sprite potatoSprite;
    public Sprite sausageSprite;
    public Sprite tomatoSprite;
    public Sprite saladSprite;
    public Sprite doughSprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateRecipeUI() {
        int i = 0;
        foreach (var d in Game.recipes[0].ingredients) {
            foreach (var a in Game.recipes[0].done) {
                if (d == a) {
                    ingredients[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    return;
                } else {
                    ingredients[i].GetComponent<Image>().color = new Color(1, 1, 1, .5f);
                }
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var elem in Game.recipes[0].ingredients) {
            if (elem == "potato") {
                ingredients[i].GetComponent<Image>().sprite = potatoSprite;
            } else if (elem == "sausage") {
                ingredients[i].GetComponent<Image>().sprite = sausageSprite;
            } else if (elem == "tomato") {
                ingredients[i].GetComponent<Image>().sprite = tomatoSprite;
            } else if (elem == "salad") {
                ingredients[i].GetComponent<Image>().sprite = saladSprite;
            } else if (elem == "dough") {
                ingredients[i].GetComponent<Image>().sprite = doughSprite;
            }
            i ++;
        }
        UpdateRecipeUI();       

    }
}
