using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public ingredientTypes type;
    public GameObject destroyParticles;

    public float speed;
    private Tilemap tilemap;
    private Rigidbody2D enemybody;
    public int state; // 1  patrol 2  panic 3  just spawned
    public bool kitchen;
    int count;
    List<Vector3> path;
    float time;

    private Vector3 change; 
    public bool right;
    Vector3 startPos;
    Vector3 endPos;
    SoundEffectMixer sound;
    public GameObject seed;


    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        endPos = new Vector3(0,0,0);
        state = 3;
        count = 0;
        time = 0;
        kitchen = false;
        enemybody = GetComponent<Rigidbody2D>();
        right = true;
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffectMixer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && state != 3){
            state = 2;
            time = 0;
            time = time + Time.deltaTime;
        }
 
        if (state == 1)
        {
           if (right) {
			    transform.Translate(2* Time.deltaTime * speed, 0,0);
			    transform.localScale= new Vector2 (1,1);
 		    }
		    else if(!right) {
			    transform.Translate(-2* Time.deltaTime * speed, 0,0);
			    transform.localScale= new Vector2 (-1,1);
		    }
        } else if (state == 2 && state != 3){
            time = time + Time.deltaTime;
            float fastspeed = 1.5F;

            if (right) {
			    transform.Translate(2* Time.deltaTime * fastspeed, 0,0);
			    transform.localScale= new Vector2 (1,1);
 		    }
		    else if(!right) {
			    transform.Translate(-2* Time.deltaTime * fastspeed, 0,0);
			    transform.localScale= new Vector2 (-1,1);
		    }

            if (time >= 5){
                state = 1;
                time = 0;
            }

        } else if (state == 3)
        {
            if (count == 0){
                path = AStar.FindPath(tilemap, transform.position, endPos);
                count= 1;
            }
            if (count < path.Count){
            transform.position += (path[count] - transform.position).normalized * Time.deltaTime * speed;
                if(Vector3.Distance(transform.position,path[count]) < 0.1)
                    count = count + 1;
            } else {
                count = 0;
                state = 1;
            }
        }

    
    }


    private void OnTriggerEnter2D(Collider2D obstacle) {
        if (obstacle.gameObject.CompareTag("Wall")){
            if (right){
				right = false;
			}
			else{
				right = true;
			}
        } else if (obstacle.gameObject.CompareTag("Kitchen"))
            kitchen = true;
        else if (obstacle.gameObject.CompareTag("Ingredient"))
        {
            obstacle.gameObject.GetComponent<Ingredient>().Death();
        } else if (obstacle.gameObject.CompareTag("Seed")) {
            obstacle.gameObject.GetComponent<Seed>().Death();
        }
    }


    public void Death() {
        // Splash
        // Death Animation 1-2 sec
        // Then spawn an ingredient from type
        // Then destroy this entity
        sound.MakeSprotchSound();
        Instantiate(seed, transform.position, Quaternion.identity);
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
