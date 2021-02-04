using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] imgs;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = imgs[Random.Range(0, imgs.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
