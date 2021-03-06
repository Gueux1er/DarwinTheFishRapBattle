﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class MovementCharacter : MonoBehaviour
{
    public static MovementCharacter Instance;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject combatUI;
    [SerializeField] CombatScript cS;
    ChallengerStat challengerInfos;
    [HideInInspector] public bool inFight = false;
    [HideInInspector] public bool prepareFight = false;
    [HideInInspector] public bool canMove = false;

    public Animator animator;

    [EventRef]
    public string newChallengerSound;
    [EventRef]
    public string questionsSound;

    public EventInstance newChallengerSoundInstance;
    private EventInstance questionsSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        newChallengerSoundInstance = RuntimeManager.CreateInstance(newChallengerSound);
        questionsSoundInstance = RuntimeManager.CreateInstance(questionsSound);

        if (ChallengerManager.Instance.challengers.Count > LevelManager.Instance.level)
            StartSoundQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (inFight == false && !prepareFight)
        {
            if (canMove == true)
                Move();
        }
        else if (inFight)
        {
            if (cS.slider.fillAmount > 0.7f)
                cS.slider.fillAmount -= challengerInfos.powerFlow * 0.065f * Time.deltaTime;
            else if (cS.slider.fillAmount < 0.2f)
                cS.slider.fillAmount -= challengerInfos.powerFlow * 0.015f * Time.deltaTime;
            else
                cS.slider.fillAmount -= challengerInfos.powerFlow * 0.035f * Time.deltaTime;
            cS.handleSlider.value = cS.slider.fillAmount;

            ChallengerManager.Instance.currentChallenger.musicSoundInstance.setParameterValue("Parameter 1", cS.handleSlider.value);
        }
        if (this.transform.position.x > 30f)
        {
            canMove = false;
            this.transform.position = new Vector3(-25, this.transform.position.y, this.transform.position.z);
            LevelManager.Instance.level += 1;
            LevelManager.Instance.EndFade();
        }
    }

    void Move()
    {
        this.transform.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Challenger")
        {
            prepareFight = true;

            challengerInfos = collision.transform.GetComponent<ChallengerStat>();
            CombatSetup(true);

            StopSoundQuestion();
        }
    }

    public void CombatSetup(bool changePosition)
    {
        combatUI.SetActive(true);
        cS.StartFight(challengerInfos.name, challengerInfos.flavorText, challengerInfos.spriteToDisplay,changePosition);
    }

    private bool questionSoundReady;
    public void StartSoundQuestion()
    {
        if (questionSoundReady)
            return;

        questionSoundReady = true;

        animator.SetBool("Idle", false);
        questionsSoundInstance.start();
    }

    public void StopSoundQuestion()
    {
        questionSoundReady = false;

        animator.SetBool("Idle", true);
        questionsSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
