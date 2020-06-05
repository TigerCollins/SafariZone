using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string newSceneName = "null";

    [Header("Visual Aspect")]
    public GameObject currentScenePanel;
    public GameObject nextScenePanel;
    public Color currentSceneColour;
    public Color nextSceneColour;

    [Header("Animation")]
    public float animationTime;
    public Animator currentSceneAnimator;


    private void Awake()
    {
       // gameObject.SetActive(true);
    }

    void Start()
    {
        //Colour change
        currentScenePanel.gameObject.GetComponent<Image>().color = currentSceneColour;
        nextScenePanel.gameObject.GetComponent<Image>().color = nextSceneColour;
        StartCoroutine("ClosePanelAnimation");
    }


    public void TriggerSceneChange(string sceneName)
    {
        newSceneName = sceneName;
        //Colour change
        currentScenePanel.gameObject.GetComponent<Image>().color = currentSceneColour;
        nextScenePanel.gameObject.GetComponent<Image>().color = nextSceneColour;

        //Animation
        currentSceneAnimator.SetBool("isCovered", true);
        StartCoroutine("PanelAnimation");
    }

    IEnumerator PanelAnimation()
    {
        yield return new WaitForSecondsRealtime(animationTime);
        SceneManager.LoadScene(newSceneName);
    }

    IEnumerator ClosePanelAnimation()
    {
        //Colour change
        currentScenePanel.gameObject.GetComponent<Image>().color = currentSceneColour;
        nextScenePanel.gameObject.GetComponent<Image>().color = nextSceneColour;
        yield return new WaitForSecondsRealtime(animationTime);
        currentSceneAnimator.SetBool("isCovered", false);
    }

    public void OpenPanelNoSceneChange()
    {
        currentSceneAnimator.SetBool("isCovered", true);
    }

    public void ClosePanelNoSceneChange()
    {
        currentSceneAnimator.SetBool("isCovered", false);
    }
}
