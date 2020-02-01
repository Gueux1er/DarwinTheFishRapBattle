using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelLecteur : MonoBehaviour
{
    [SerializeField] string[] darwinPunchLines;
    [SerializeField] string[] kaariscargotPunchlines;
    [SerializeField] string[] carpenterSlugPunchlines;
    [SerializeField] string[] theCarPunchLines;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset PunchlinesKaaris = Resources.Load<TextAsset>("KaariscargotPunchlines");
        kaariscargotPunchlines = PunchlinesKaaris.text.Split(new char[] { '\n' });

        TextAsset PunchlinesCarpenter = Resources.Load<TextAsset>("KaariscargotPunchlines");
        kaariscargotPunchlines = PunchlinesCarpenter.text.Split(new char[] { '\n' });

        TextAsset PunchlinesTheCar = Resources.Load<TextAsset>("KaariscargotPunchlines");
        kaariscargotPunchlines = PunchlinesTheCar.text.Split(new char[] { '\n' });

        TextAsset darwinPunchLines = Resources.Load<TextAsset>("KaariscargotPunchlines");
        kaariscargotPunchlines = darwinPunchLines.text.Split(new char[] { '\n' });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
