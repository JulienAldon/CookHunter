using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tables;
    private bool once = true;
    public GameObject endlevel;
    public SoundEffectMixer sound;

    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundEffectMixer>();

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
        if (CheckService() && once) {
            once = false;
            // Show levelcomplete text 
            
            //call scoremanager end level -> back to menu
            endlevel.SetActive(true);
            sound.MakeLevelClearSound();
        } 
    }
}
