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
        MakeSound(ShotSound);
    }

    public void MakeGrabSound()
    {
        MakeSound(GrabSound);
    }

    public void MakeVictorySound()
    {
        MakeSound(VictorySound);
    }

    public void MakeDefaitSound()
    {
        MakeSound(DefaitSound);
    }
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
