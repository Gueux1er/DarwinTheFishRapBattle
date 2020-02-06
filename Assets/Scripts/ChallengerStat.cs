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

    public GameObject explosionParticlePrefab;

    public bool interrupt;

    [EventRef]
    public string musicSound;
    [EventRef]
    public string dieSound;

    public EventInstance musicSoundInstance;
    public EventInstance dieSoundInstance;

    public float powerFlow;

    [Header("For CAR")]
    public GameObject entryCarObject;
    public GameObject idleCarObject;

    private void Awake()
    {
        musicSoundInstance = RuntimeManager.CreateInstance(musicSound);
        dieSoundInstance = RuntimeManager.CreateInstance(dieSound);
    }

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

        StartCoroutine(CatchlinesCoco());
    }

    public void StopPunchLines()
    {
        StopCoroutine(CatchlinesCoco());
        ChallengerManager.Instance.DisableOnCatchlines();
    }

    IEnumerator CatchlinesCoco()
    {
        ChallengerManager.Instance.EnableOneCatchline();

        while (true)
        {
            yield return new WaitForSeconds(6f);

            ChallengerManager.Instance.DisableOnCatchlines();
            ChallengerManager.Instance.EnableOneCatchline();

            if (!MovementCharacter.Instance.inFight)
            {
                ChallengerManager.Instance.DisableOnCatchlines();
                yield break;
            }
        }
    }

    private bool isDead;
    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        ChallengerManager.Instance.DisableOnCatchlines();

        StopCoroutine(CatchlinesCoco());

        dieSoundInstance.start();
        Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.2f);
    }
}
