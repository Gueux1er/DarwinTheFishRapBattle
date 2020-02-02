using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class textManager : Singleton<textManager>
{
    bool godMode = false;

    [SerializeField] Text preText;
    [SerializeField] Text wordInProduction;

    [SerializeField] string keyPressed;

    //[SerializeField] List<string> punchLines = new List<string>();
    [SerializeField] List<string> possibleEndOfPunchLines = new List<string>();

    int count;
    [SerializeField] float multiplicator = 1;
    [SerializeField] float multiplicatorSpeed = 0.25f;
    [SerializeField] float basePoints;

    [SerializeField] CombatScript cS;
    [SerializeField] ExcelLecteur eL;

    [SerializeField] GameObject victoryCanvas;
    [SerializeField] GameObject looseCanvas;

    [EventRef]
    public string validSound;
    [EventRef]
    public string errorSound;

    private EventInstance validSoundInstance;
    private EventInstance errorSoundInstance;

    bool nextWord = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        validSoundInstance = RuntimeManager.CreateInstance(validSound);
        errorSoundInstance = RuntimeManager.CreateInstance(errorSound);

        preText.text = null;
        new WaitForEndOfFrame();
        while (true)
        {
            if (preText.text.Length < 25)
            {
                preText.text += " " + eL.darwinPunchLines[Random.Range(0, eL.darwinPunchLines.Length - 1)];
                //punchLines.RemoveAt(count);
            }
            else
            {
                return null;
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (cS.slider.fillAmount == 0)
        {
            YouLoose();
        }else if (cS.slider.fillAmount == 1)
        {
            YouWin();
        }



        if (Input.anyKeyDown)
            if (Input.inputString != "")
                CompareKeyPressedToText(Input.inputString);

        if (preText.text[0] == " "[0])
        {
            WordFinish();
        }
    }

    void YouWin()
    {
        ChallengerManager.Instance.currentChallenger.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        MovementCharacter.Instance.StartSoundQuestion();
    }

    void YouLoose()
    {
        cS.slider.fillAmount = 0.95f;
        cS.gameObject.SetActive(false);
        looseCanvas.SetActive(true);
    }

    void CheckForEndOfPunchLines()
    {
        for (int i =0; i < possibleEndOfPunchLines.Count; i++)
        {
            if (possibleEndOfPunchLines[i][0] == preText.text.ToLower()[0])
            {
                EndOfPunchLines();
            }
        }
    }

    void EndOfPunchLines()
    {
        print("EndOfPunchLines");
    }


    void CompareKeyPressedToText(string keyPressed)
    {
        if (keyPressed == "²")
        {
            godMode = !godMode;
            print("GodMode : " + godMode);
        }
        if (keyPressed[0] == " "[0] && nextWord == true)
        {
            return;
        }
        if (keyPressed.ToLower()[0] == preText.text.ToLower()[0] || godMode == true)
        {
            CheckForEndOfPunchLines();
            wordInProduction.text += "<color=green>" + preText.text[0] + "</color>";
            preText.text = preText.text.Substring(1);
            nextWord = false;
            multiplicator += multiplicatorSpeed;
            cS.slider.fillAmount += basePoints * multiplicator;
            cS.handleSlider.value += basePoints * multiplicator;
            validSoundInstance.start();
        }
        else
        {
            wordInProduction.text += "<color=red>" + preText.text[0] + "</color>".ToUpper();
            wordInProduction.text = wordInProduction.text;
            preText.text = preText.text.Substring(1);
            nextWord = false;
            multiplicator = 1;
            errorSoundInstance.start();
        }

        if (multiplicator > 2)
        {
            multiplicator = 2f;
        }

        CheckForLenghtOfText();
    }

    void CheckForLenghtOfText()
    {
        if (preText.text.Length < 35)
        {
            preText.text += " " + eL.darwinPunchLines[Random.Range(0, eL.darwinPunchLines.Length - 1)];
            //punchLines.RemoveAt(count);
        }
    }

    void WordFinish()
    {
        preText.text = preText.text.Substring(1);
        wordInProduction.text = null;
        nextWord = true;
    }
}
