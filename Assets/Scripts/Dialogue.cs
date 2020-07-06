using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool gameOverMessage;
    public string NPCName;

    [TextArea(2,5)]
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {

    }

}
