using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ingredientTypes type;
    public GameObject destroyParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death() {
        // Splash
        // Death Animation 1-2 sec
        // Then spawn an ingredient from type
        // Then destroy this entity
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
