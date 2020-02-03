using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using FMODUnity;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class PerfectTextManager : MonoBehaviour
{
    

    [SerializeField] GameObject[] textToCheck;
    [SerializeField] int[] goodPart;

    [SerializeField] string textSend;

    [SerializeField] MovementCharacter movementCharacter;
    [SerializeField] CombatScript cS;

    [EventRef]
    public string validSound;

    private EventInstance validSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        validSoundInstance = RuntimeManager.CreateInstance(validSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            CompareKeyPressedToText(Input.inputString);

        if (textSend != "")
        {
            WhatToDo();
        }
    }


    void CompareKeyPressedToText(string textToCompare)
    {
        print(textToCheck.Length);
        for (int i = 0; i < textToCheck.Length; i++)
        {
            print(i);
            if (textToCheck[i].transform.GetChild(i).GetComponent<Text>() != null &&
                textToCompare.ToLower()[0] == textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text.ToLower()[0])
            {
                textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text = "<color=green>" + textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text + "</color>";

                goodPart[i] += 1;

                if (goodPart[i] == textToCheck[i].transform.childCount)
                {
                    textSend = textToCheck[i].name;
                }

                validSoundInstance.start();
            }
            else if (textToCheck[i].transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null &&
                textToCompare.ToLower()[0] == textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<TextMeshProUGUI>().text.ToLower()[0])
            {
                textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<TextMeshProUGUI>().text = "<color=green>" + textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<TextMeshProUGUI>().text + "</color>";

                goodPart[i] += 1;
                if (goodPart[i] == textToCheck[i].transform.childCount)
                {
                    textSend = textToCheck[i].name;
                }

                validSoundInstance.start();
            }
        }
    }

    void CleanAll()
    {
        for (int i =0; i< textToCheck.Length; i++)
        {
            for (int j=0; j < textToCheck[i].transform.childCount -1; j++)
            {
                if (textToCheck[i].transform.GetChild(j).GetComponent<Text>() != null)
                    textToCheck[i].transform.GetChild(j).GetComponent<Text>().text = textToCheck[i].transform.GetChild(j).name;
                else if (textToCheck[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    textToCheck[i].transform.GetChild(j).GetComponent<TextMeshProUGUI>().text = textToCheck[i].transform.GetChild(j).name;
            }
        }
        for (int i = 0; i < goodPart.Length; i++)
        {
            goodPart[i] = 0;
        }
    }

    void WhatToDo()
    {
        if (textSend == "Retry")
        {
            ChallengerManager.Instance.currentChallenger?.musicSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene(1);
        }
        else if (textSend == "Abandon")
        {
            Application.Quit();
        }else if (textSend == "START")
        {
            print("Launch other");
            SceneManager.LoadScene(1);
        }else if (textSend == "Fin")
        {
            print("quit");
            Application.Quit();
        }




        //textSend = "";
    }
}
