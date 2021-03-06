﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    static public LevelManager Instance;
    public int level;
    public CanvasGroup fadeCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        BeginFade();
    }


    void BeginFade()
    {
        LevelManager.Instance.fadeCanvas.alpha = 1;
        LevelManager.Instance.fadeCanvas.DOFade(0, 2f)
            .OnComplete(() => MovementCharacter.Instance.canMove = true);
    }

    public void EndFade()
    {
        LevelManager.Instance.fadeCanvas.alpha = 0;
        LevelManager.Instance.fadeCanvas.DOFade(1, 2f)
            .OnComplete(() =>
            {
                ChallengerManager.Instance.currentChallenger?.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                MovementCharacter.Instance.StopSoundQuestion();
                SceneManager.LoadScene(1);
            });

    }
    // Update is called once per frame
    void Update()
    {
    }
}
