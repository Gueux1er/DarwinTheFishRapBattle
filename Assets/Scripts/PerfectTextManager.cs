using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectTextManager : MonoBehaviour
{
    

    [SerializeField] GameObject[] textToCheck;
    [SerializeField] int[] goodPart;

    [SerializeField] string textSend;

    [SerializeField] MovementCharacter movementCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
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
        for (int i = 0; i < textToCheck.Length; i++)
        {
            if (textToCompare.ToLower()[0] == textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text.ToLower()[0])
            {
                textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text = "<color=green>" + textToCheck[i].transform.GetChild(goodPart[i]).GetComponent<Text>().text + "</color>";
                goodPart[i] += 1;
                if (goodPart[i] == textToCheck[i].transform.childCount)
                {
                    textSend = textToCheck[i].name;
                }
            }
        }
    }

    void WhatToDo()
    {
        if (textSend == "Retry")
        {
            movementCharacter.CombatSetup();
            textSend = "";
            this.gameObject.SetActive(false);
        }
        else if (textSend == "Abandon")
        {
            Application.Quit();
        }



        textSend = "";
    }
}
