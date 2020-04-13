using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManagerVisualiser : MonoBehaviour
{
    public GraphicsManager graphicsManager;

    public Toggle chromaticAberrationOn;
    public Toggle chromaticAberrationOff;
    public Toggle filmGrainOn;
    public Toggle filmGrainOff;
    public Toggle vignetteOn;
    public Toggle vignetteOff;
    public ToggleGroup toggleGroup;


    // Start is called before the first frame update
    void Awake()
    {
        //ChromaticAberrationChange();
        //ChromaticAberrationStartup();
      //  FilmGrainStartup();
      // VignetteStartup();
        //toggleGroup.
    }

    public void Update()
    {
        
    }

    // ChromaticAberration
    public void ChromaticAberrationStartup()
    {
        if(graphicsManager.chromaticAberration == true)
        {
            chromaticAberrationOff.isOn = false;
            chromaticAberrationOn.isOn = true;
        }
        else
        {
            chromaticAberrationOn.isOn = false;
            chromaticAberrationOff.isOn = true;
        }
    }

    public void ChromaticAberrationChange()
    {
        if(chromaticAberrationOn.isOn)
        {
            graphicsManager.chromaticAberration = true;
            graphicsManager.SetGlobalChromaticAberration();
        }

        else
        {
            graphicsManager.chromaticAberration = false;
            graphicsManager.SetGlobalChromaticAberration();
        }
    }

    // FilmGrain
    public void FilmGrainStartup()
    {
        if (graphicsManager.filmGrain)
        {
            filmGrainOn.isOn = true;
        }
        else
        {
            filmGrainOff.isOn = true;
        }
    }

    public void FilmGrainChange()
    {
        if (filmGrainOn.isOn)
        {
            graphicsManager.filmGrain = true;
            graphicsManager.SetGlobalFilmGrain();
        }

        else
        {
            graphicsManager.filmGrain = false;
            graphicsManager.SetGlobalFilmGrain();
        }
    }

    // Vignette
    public void VignetteStartup()
    {
        if (graphicsManager.vignette)
        {
            filmGrainOn.isOn = true;
        }
        else
        {
            filmGrainOff.isOn = true;
        }
    }

    public void VignetteChange()
    {
        if (filmGrainOn.isOn)
        {
            graphicsManager.vignette = true;
            graphicsManager.SetGlobalVignette();
        }

        else
        {
            graphicsManager.vignette = false;
            graphicsManager.SetGlobalVignette();
        }
    }
}
