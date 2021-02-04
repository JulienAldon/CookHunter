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
    public Transform groundDetection;
    public bool movingRight = true;
    public float speed;
    public float distance;
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
        if (movingRight == true) {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // movement & behavior
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false) {
            if (movingRight == true) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
