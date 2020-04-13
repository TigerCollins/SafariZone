using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public bool lookAtCamera;
    public bool inverse;
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        if (lookAtCamera)
        {
            targetObject = FindObjectOfType<Camera>().transform.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inverse)
        {
            transform.LookAt(2 * transform.position - targetObject.transform.position);
        }
       
    }

    void ChangeTargetObject(GameObject newTargetObject)
    {
        targetObject = newTargetObject;
    }
}