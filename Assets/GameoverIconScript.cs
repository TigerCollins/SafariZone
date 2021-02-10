using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverIconScript : MonoBehaviour
{
    public Image imagePrefab;
    public GameoverController gameoverController;
    // Start is called before the first frame update
    void Start()
    {
        gameoverController = FindObjectOfType<GameoverController>();
        imagePrefab = GetComponent<Image>();
        if (transform.parent.name == "Image Group Obtained")
        {
            imagePrefab.sprite = gameoverController.itemsObtained[transform.GetSiblingIndex()].icon;
        }

        if (transform.parent.name == "Image Group Used")
        {
            imagePrefab.sprite = gameoverController.itemsUsed[transform.GetSiblingIndex()].icon;
        }

        if (transform.parent.name == "Image Group Creature")
        {
            imagePrefab.sprite = gameoverController.creaturesObtained[transform.GetSiblingIndex()].creatureIcon;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
