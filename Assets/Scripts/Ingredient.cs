using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string type;
    public Sprite potatoSprite;
    public Sprite sausageSprite;
    public Sprite tomatoSprite;
    public Sprite saladSprite;
    public Sprite doughSprite;
    // Start is called before the first frame update
    void Start()
    {
        if (type == "potato") {
            GetComponent<SpriteRenderer>().sprite = potatoSprite;
        }
        if (type == "sausage") {
            GetComponent<SpriteRenderer>().sprite = sausageSprite;
        }
        if (type == "tomato") {
            GetComponent<SpriteRenderer>().sprite = tomatoSprite;
        }
        if (type == "salad") {
            GetComponent<SpriteRenderer>().sprite = saladSprite;
        }
        if (type == "dough") {
            GetComponent<SpriteRenderer>().sprite = doughSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // movement & behavior
    }
}
