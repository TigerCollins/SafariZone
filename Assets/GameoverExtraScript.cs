using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverExtraScript : MonoBehaviour
{
    public Text extraPrefab;
    public Image imagePrefab;
    public GameoverController gameoverController;
    // Start is called before the first frame update
    void Start()
    {
        gameoverController = FindObjectOfType<GameoverController>();
       
        int count = 0;
        if(transform.parent.name == "Image Group Creature" || transform.parent.name == "Image Group Obtained" || transform.parent.name == "Creatures Caught")
        {
            if(transform.parent.childCount >1)
            {
                count = gameoverController.creaturesObtained.Count - 4;
            }
            
            else
            {
                count = 0;
            }
        }

        else
        {
            count = 0;
        }

        extraPrefab.text = "+" + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
