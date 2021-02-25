using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectMixer : MonoBehaviour
{
    public static SoundEffectMixer Instance;

    public AudioClip ShotSound;
    public AudioClip GrabSound;
    public AudioClip VictorySound;
    public AudioClip DefaitSound;
    public AudioClip sprotchSound;
    public AudioClip levelClearSound;
    


    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectMixer");
        }
        Instance = this;
    }

    public void MakeShotSound()
    {
        MakeSound(ShotSound, .5f);
    }

    public void MakeGrabSound()
    {
        MakeSound(GrabSound, 1f);
    }

    public void MakeVictorySound()
    {
        MakeSound(VictorySound, .7f);
    }

    public void MakeDefaitSound()
    {
        MakeSound(DefaitSound, 1f);
    }

    public void MakeSprotchSound()
    {
        MakeSound(sprotchSound, .5f);
    }
    
    public void MakeLevelClearSound()
    {
        MakeSound(levelClearSound, 1f);
    }
    
    private void MakeSound(AudioClip originalClip, float volume)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position, volume);
    }
}
