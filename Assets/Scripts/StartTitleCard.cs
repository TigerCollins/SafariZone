﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTitleCard : MonoBehaviour
{
    public FirstPlaythrough firstPlaythrough;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBool()
    {
        firstPlaythrough.titlecardTriggered = true;
        firstPlaythrough.playerProfile.firstTime = false;
    }

}