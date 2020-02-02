using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class ChallengerManager : MonoBehaviour
{
    public static ChallengerManager Instance;

    public List<ChallengerStat> challengers;
    public ChallengerStat currentChallenger;

    public ChallengerStat carChallenger;

    public TextMeshPro punchlinesText;

    [EventRef]
    public string wallSound;
    public EventInstance wallSoundInstance;

    public void Start()
    {
        Instance = this;

        carChallenger.gameObject.SetActive(false);

        for (int i = 0; i < challengers.Count; ++i)
        {
            challengers[i].gameObject.SetActive(false);
        }
        if (challengers.Count > LevelManager.Instance.level)
        {
            currentChallenger = challengers[LevelManager.Instance.level];
            currentChallenger.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2);
            MovementCharacter.Instance.StopSoundQuestion();
        }

        wallSoundInstance = RuntimeManager.CreateInstance(wallSound);
    }

    public void CheckInterruptFight()
    {
        if (currentChallenger.interrupt)
        {
            StartCoroutine(TimerInterruption());
        }
    }

    public void DisableOnCatchlines()
    {
        punchlinesText.text = string.Empty;
    }

    public void EnableOneCatchline()
    {
        punchlinesText.color = Color.white;
        punchlinesText.text =
            ExcelLecteur.Instance.currentPunchLines[Random.Range(0, ExcelLecteur.Instance.currentPunchLines.Length)];

        punchlinesText.DOColor(Color.clear, 1f).SetDelay(4.5f);
    }

    IEnumerator TimerInterruption()
    {
        yield return new WaitForSeconds(5f);

        wallSoundInstance.start();

        DestroyWall.Instance.Destroy();

        currentChallenger.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        carChallenger.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        currentChallenger.Die();

        yield return new WaitForSeconds(1.5f);

        textManager.Instance.YouWin(false);

        currentChallenger = carChallenger;
    }
}
