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

    [EventRef]
    public string musicSound;
    public EventInstance musicSoundInstance;


    public float powerFlow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
