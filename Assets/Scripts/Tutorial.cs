using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tutorial
{
    public string tutorialName;

    [TextArea(2, 4)]
    public string[] sentences;
}
