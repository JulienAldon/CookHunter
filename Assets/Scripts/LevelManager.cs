using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tables;

    void Start()
    {

    }

    bool CheckService() {
        foreach (var elem in tables) {
            if (elem.GetComponent<Table>().satisfied == false) {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (CheckService()) {
            Debug.Log("victory");
            // Show levelcomplete text 
            
            //call scoremanager end level -> back to menu
        } 
    }
}
