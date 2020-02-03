using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class textManager : MonoBehaviour
{
    public static textManager Instance;

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
    [SerializeField] MovementCharacter mC;

    [SerializeField] GameObject victoryCanvas;
    [SerializeField] GameObject looseCanvas;

    [EventRef]
    public string validSound;
    [EventRef]
    public string errorSound;

    private EventInstance validSoundInstance;
    private EventInstance errorSoundInstance;

    bool nextWord = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        GetAllRef();
        validSoundInstance = RuntimeManager.CreateInstance(validSound);
        errorSoundInstance = RuntimeManager.CreateInstance(errorSound);

        preText.text = null;
        new WaitForEndOfFrame();
        while (true)
        {
            if (preText.text.Length < 35)
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

    void GetAllRef()
    {
        /*preText = GameObject.Find("Partie Non Fini").GetComponent<Text>();
        wordInProduction = GameObject.Find("Partie Mot Fini").GetComponent<Text>();
        cS = GameObject.Find("CanvasCBT").GetComponent<CombatScript>();
        eL = GameObject.Find("ExcelManager").GetComponent<ExcelLecteur>();
        mC = GameObject.Find("Avatar").GetComponent<MovementCharacter>();
        looseCanvas = GameObject.Find("DefeatCanvas");*/
    }

    // Update is called once per frame
    void Update()
    {

        if (cS.slider.fillAmount == 0)
        {
            YouLoose();
        }
        if (cS.slider.fillAmount >= 0.95)
        {
            print("win");
            YouWin();
        }



        if (Input.anyKeyDown && mC.inFight)
            if (Input.inputString != "")
                CompareKeyPressedToText(Input.inputString);

        if (preText.text[0] == " "[0])
        {
            WordFinish();
        }
    }

    public void YouWin(bool startSoundQuestion = true)
    {
        if (ChallengerManager.Instance.currentChallenger != null)
        {
            ChallengerManager.Instance.currentChallenger.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ChallengerManager.Instance.currentChallenger.Die();
        }

        if (startSoundQuestion)
            MovementCharacter.Instance.StartSoundQuestion();

        mC.inFight = false;
        mC.prepareFight = false;
        cS.slider.fillAmount = 0.5f;
        cS.gameObject.SetActive(false);
    }

    void YouLoose()
    {
        cS.slider.fillAmount = 0.5f;
        cS.gameObject.SetActive(false);
        looseCanvas.SetActive(true);
        mC.inFight = false;

        ChallengerManager.Instance.currentChallenger.StopPunchLines();
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
            cS.slider.fillAmount += basePoints * multiplicator*0.5f;
            cS.handleSlider.value = cS.slider.fillAmount;
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
