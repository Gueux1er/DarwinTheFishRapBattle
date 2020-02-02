using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelLecteur : MonoBehaviour
{
    public static ExcelLecteur Instance;

    public string[] darwinPunchLines;
    [SerializeField] string[] kaariscargotPunchlines;
    [SerializeField] string[] carpenterSlugPunchlines;
    [SerializeField] string[] theCarPunchLines;

    [HideInInspector] public string[] currentPunchLines;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        TextAsset darwinPunchLinesLector = Resources.Load<TextAsset>("darwinPunchLines");
        darwinPunchLines = darwinPunchLinesLector.text.Split(new char[] { '\n' });

        TextAsset PunchlinesKaaris = Resources.Load<TextAsset>("KaariscargotPunchlines");
        kaariscargotPunchlines = PunchlinesKaaris.text.Split(new char[] { '\n' });

        TextAsset PunchlinesCarpenter = Resources.Load<TextAsset>("CarpenterSlugPunchlines");
        carpenterSlugPunchlines = PunchlinesCarpenter.text.Split(new char[] { '\n' });

        TextAsset PunchlinesTheCar = Resources.Load<TextAsset>("theCarPunchLines");
        theCarPunchLines = PunchlinesTheCar.text.Split(new char[] { '\n' });
    }

    private void  Update()
    {
        if (ChallengerManager.Instance.currentChallenger != null)
        {
            switch(ChallengerManager.Instance.currentChallenger.name)
            {
                case "KAARISCARGOT":
                    currentPunchLines = kaariscargotPunchlines;
                        break;

                case "CARPENTER SLUG":
                    currentPunchLines = carpenterSlugPunchlines;
                    break;

                case "THE CAR":
                    currentPunchLines = theCarPunchLines;
                    break;

                default:
                    currentPunchLines = null;
                    break;
            }
        }
    }
}
