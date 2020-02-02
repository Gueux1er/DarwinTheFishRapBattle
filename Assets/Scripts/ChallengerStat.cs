using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class ChallengerStat : MonoBehaviour
{
    public string name;
    public string flavorText;
    public Sprite spriteToDisplay;

    public bool interrupt;

    [EventRef]
    public string musicSound;
    public EventInstance musicSoundInstance;

    public float powerFlow;

    [Header("For CAR")]
    public GameObject entryCarObject;
    public GameObject idleCarObject;

    public void OnEnable()
    {
        if (idleCarObject != null)
            idleCarObject.SetActive(false);
    }
    
    public void Fight()
    {
        if (entryCarObject != null)
            entryCarObject.SetActive(false);
        if (idleCarObject != null)
            idleCarObject.SetActive(true);
    }

    public void Die()
    {

    }
}
