using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewReposition : MonoBehaviour
{
    public Vector2[] targetObject;
    public ScrollRect scrollRect;
    public float time;
    private float currentPosX;
    private float currentPosY;
    private int arrayValue;
    private bool canDrag = true;

    public void Start()
    {
        UpdateArrayValue(1);
    }

    public void UpdateArrayValue(int newArrayValue)
    {
        arrayValue = newArrayValue;
        canDrag = false;
    }

    public void Update()
    {

        if(canDrag == true)
        {
            currentPosX = scrollRect.horizontalNormalizedPosition;
            currentPosY = scrollRect.verticalNormalizedPosition;
        }

        else
        {
            MoveScrollRect();
        }

    }

    public void MoveScrollRect()
    {
        currentPosX = Mathf.MoveTowards(currentPosX, targetObject[arrayValue].x, time * Time.deltaTime);
        currentPosY = Mathf.MoveTowards(currentPosY, targetObject[arrayValue].y, time * Time.deltaTime);
        scrollRect.horizontalNormalizedPosition = currentPosX;
        scrollRect.verticalNormalizedPosition = currentPosY;

        if (currentPosX == targetObject[arrayValue].x && currentPosY == targetObject[arrayValue].y)
        {
            canDrag = true;
        }
    }
}
