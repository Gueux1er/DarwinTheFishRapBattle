﻿using System.Collections;
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

        TextAsset PunchlinesCarpenter = Resources.Load<TextAsset>("CarpenterSlugPunchlines");
        carpenterSlugPunchlines = PunchlinesCarpenter.text.Split(new char[] { '\n' });

        TextAsset PunchlinesTheCar = Resources.Load<TextAsset>("theCarPunchLines");
        theCarPunchLines = PunchlinesTheCar.text.Split(new char[] { '\n' });

        TextAsset darwinPunchLinesLector = Resources.Load<TextAsset>("KaariscargotPunchlines");
        darwinPunchLines = darwinPunchLinesLector.text.Split(new char[] { '\n' });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
