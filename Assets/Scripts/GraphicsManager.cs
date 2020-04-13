using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GraphicsManager : MonoBehaviour
{

    public PlayerData playerData;
    public PostProcessProfile postProcessProfile;
    public Camera gameCamera;

    [Header("Option Variables")]
    public bool chromaticAberration;
    public bool filmGrain;
    public bool vignette;
    public bool dynamicResolution;
    public bool VSync;
    public TextureResolutionFactor textureResolution;
    public AntiAliasingFactor antiAliasing;
    public bool anistropicFiltering;
    public bool ambientOcclusion;
    public bool sceneChanging;

    [Header("Visual Variables")]
    public bool optionsScene;
    public float timer;
    public Toggle chromaticAberrationOn;
    public Toggle chromaticAberrationOff;
    public Toggle filmGrainOn;
    public Toggle filmGrainOff;
    public Toggle vignetteOn;
    public Toggle vignetteOff;
    public Toggle dynamicResolutionOn;
    public Toggle dynamicResolutionOff;
    public Toggle VSyncOn;
    public Toggle VSyncOff;
    public GameObject texResHolder;
    public Toggle qtrResolution;
    public Toggle halfResolution;
    public Toggle fullResolution;
    public GameObject AAToggleHolder;
    public Toggle AAOff;
    public Toggle MSAAx2;
    public Toggle MSAAx4;
    public Toggle MSAAx8;
    public Toggle MSAAx16;
    public Toggle ambientOcclusionOn;
    public Toggle ambientOcclusionOff;

    public void ChangeSceneBool()
    {
        sceneChanging = true;

    }

    private void Start()
    {
        InitialiseOptions();
    }

    private void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Options" || SceneManager.GetActiveScene().name == "World")
        {
            InitialiseOptions();
        }

            if (SceneManager.GetActiveScene().name == "Options")
        {
            optionsScene = true;
        }

        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SetLocalAmbientOcclusion();
        SetLocalAnistropicFiltering();
        SetLocalAntiAliasing();
        SetLocalChromaticAberration();
        SetLocalDynamicResolution();
        SetLocalFilmGrain();
        SetLocalTextureResolution();
        SetLocalVignette();
        SetLocalVsync();

    }

    public void InitialiseOptions()
        {
        chromaticAberration = playerData.chromaticAberration;
            filmGrain = playerData.filmGrain;
            vignette = playerData.vignette;
            dynamicResolution = playerData.dynamicResolution;
            VSync = playerData.VSync;
            textureResolution = playerData.textureResolution;
            antiAliasing = playerData.antiAliasing;
            anistropicFiltering = playerData.anistropicFiltering;
            ambientOcclusion = playerData.ambientOcclusion;
        timer = 0;
        ChromaticAberrationStartup();
        FilmGrainStartup();
        VignetteStartup();
        DynamicResolutionStartup();
        VSyncStartup();
        TextureResolutionStartup();
        AntiAliasingStartup();
        AmbientOcclusionStartUp();
        sceneChanging = false;
    }

    public void Update()
    {
        timer += Time.unscaledDeltaTime;
    }

    //Chromatic Aberration
    public void SetLocalChromaticAberration()
    {
        chromaticAberration = playerData.chromaticAberration;
        //ChromaticAberrationStartup();
    }

    public void SetGlobalChromaticAberration()
    {
        if (!sceneChanging && timer > .3f)
        {
            postProcessProfile.GetSetting<ChromaticAberration>().active = chromaticAberration;
            playerData.chromaticAberration = chromaticAberration;
        }
    }

    //Film Grain
    public void SetLocalFilmGrain()
    {
        filmGrain = playerData.filmGrain;
    }

    public void SetGlobalFilmGrain()
    {
        if (!sceneChanging && timer > .3f)
        {
            postProcessProfile.GetSetting<Grain>().active = filmGrain;
            playerData.filmGrain = filmGrain;
        }
    }

    //Vignette
    public void SetLocalVignette()
    {
        vignette = playerData.vignette;
    }

    public void SetGlobalVignette()
    {
        if (!sceneChanging && timer > .3f)
        {
            postProcessProfile.GetSetting<Vignette>().active = vignette;
            playerData.vignette = vignette;
        }

    }

    //Dynamic Resolution
    public void SetLocalDynamicResolution()
    {
        dynamicResolution = playerData.dynamicResolution;
    }

    public void SetGlobalDynamicResolution()
    {
        if (!sceneChanging && timer > .3f)
        {
            gameCamera.allowDynamicResolution = dynamicResolution;
            playerData.dynamicResolution = dynamicResolution;
        }
    }

    //VSync
    public void SetLocalVsync()
    {
        VSync = playerData.VSync;
    }

    public void SetGlobalVsync()
    {
        if (!sceneChanging && timer > .3f)
        {
            if (!VSync)
            {
                QualitySettings.vSyncCount = 0;
                playerData.VSync = false;
            }

            else
            {
                QualitySettings.vSyncCount = 1;
                playerData.VSync = true;
            }
        }
    }

    //Texture Resolution
    public void SetLocalTextureResolution()
    {
        textureResolution = playerData.textureResolution;
    }

    public void SetGlobalTextureResolution()
    {
        if (texResHolder.transform.childCount == 3 && !sceneChanging && timer > .3f)
        {
            if (textureResolution == TextureResolutionFactor.low)
            {
                QualitySettings.masterTextureLimit = 2;
                playerData.textureResolution = textureResolution;
            }

            if (textureResolution == TextureResolutionFactor.Medium)
            {
                QualitySettings.masterTextureLimit = 1;
                playerData.textureResolution = textureResolution;
            }

            if (textureResolution == TextureResolutionFactor.High)
            {
                QualitySettings.masterTextureLimit = 0;
                playerData.textureResolution = textureResolution;
            }
        }
    }

    //Anti Aliasing
    public void SetLocalAntiAliasing()
    {
        antiAliasing = playerData.antiAliasing;
    }

    public void SetGlobalAntiAliasing()
    {
        if(AAToggleHolder.transform.childCount == 5 && !sceneChanging && timer > .3f)
        {
            if (antiAliasing == AntiAliasingFactor.None)
            {
                QualitySettings.antiAliasing = 0;
                playerData.antiAliasing = antiAliasing;
            }

            if (antiAliasing == AntiAliasingFactor.MSAAx2)
            {
                QualitySettings.antiAliasing = 2;
                playerData.antiAliasing = antiAliasing;
            }

            if (antiAliasing == AntiAliasingFactor.MSAAx4)
            {
                QualitySettings.antiAliasing = 4;
                playerData.antiAliasing = antiAliasing;
            }

            if (antiAliasing == AntiAliasingFactor.MSAAx8)
            {
                QualitySettings.antiAliasing = 8;
                playerData.antiAliasing = antiAliasing;
            }

            if (antiAliasing == AntiAliasingFactor.MSAAx16)
            {
                QualitySettings.antiAliasing = 16;
                playerData.antiAliasing = antiAliasing;
            }
        }
        
    }

    //Anistropic Filtering
    public void SetLocalAnistropicFiltering()
    {
        anistropicFiltering = playerData.anistropicFiltering;
    }

    public void SetGlobalAnistropicFiltering()
    {
        if (!sceneChanging && timer > .3f)
        {
            if (!anistropicFiltering)
            {
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                playerData.anistropicFiltering = anistropicFiltering;
            }

            else
            {
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                playerData.anistropicFiltering = anistropicFiltering;
            }
        }
    }

    //Ambient Occlusion
    public void SetLocalAmbientOcclusion()
    {
        ambientOcclusion = playerData.ambientOcclusion;
    }

    public void SetGlobalAmbientOcclusion()
    {
        if(!sceneChanging && timer > .3f)
        {
            postProcessProfile.GetSetting<AmbientOcclusion>().active = ambientOcclusion;
            playerData.ambientOcclusion = ambientOcclusion;
        }

    }


    //VISUAL
    public void ChromaticAberrationStartup()
    {
        if (chromaticAberration == true)
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
    public void ChromaticAberrationChange(bool isOn)
    {
        if(timer > .3f)
        {
            if (chromaticAberrationOn.isOn)
            {
                chromaticAberration = isOn;
                SetGlobalChromaticAberration();
            }

            else
            {
                chromaticAberration = isOn;
                SetGlobalChromaticAberration();
            }
        }
       
    }

    public void FilmGrainStartup()
    {
        if (filmGrain == true)
        {
            filmGrainOff.isOn = false;
            filmGrainOn.isOn = true;
        }
        else
        {
            filmGrainOn.isOn = false;
            filmGrainOff.isOn = true;
        }
    }
    public void FilmGrainChange(bool isOn)
    {
        if (timer > .3f)
        {
            if (filmGrainOn.isOn)
            {
                filmGrain = isOn;
                SetGlobalFilmGrain();
            }

            else
            {
                filmGrain = isOn;
                SetGlobalFilmGrain();
            }
        }

    }

    public void VignetteStartup()
    {
        if (vignette == true)
        {
            vignetteOff.isOn = false;
            vignetteOn.isOn = true;
        }
        else
        {
            vignetteOn.isOn = false;
            vignetteOff.isOn = true;
        }
    }
    public void VignetteChange(bool isOn)
    {
        if (timer > .3f)
        {
            if (vignetteOn.isOn)
            {
                vignette = isOn;
                SetGlobalVignette();
            }

            else
            {
                vignette = isOn;
                SetGlobalVignette();
            }
        }

    }

    public void DynamicResolutionStartup()
    {
        if (dynamicResolution == true)
        {
            dynamicResolutionOff.isOn = false;
            dynamicResolutionOn.isOn = true;
        }
        else
        {
            dynamicResolutionOn.isOn = false;
            dynamicResolutionOff.isOn = true;
        }
    }
    public void DynamicResolutionChange(bool isOn)
    {
        if (timer > .3f)
        {
            if (dynamicResolutionOn.isOn)
            {
                dynamicResolution = isOn;
                SetGlobalDynamicResolution();
            }

            else
            {
                dynamicResolution = isOn;
                SetGlobalDynamicResolution();
            }
        }

    }

    public void TextureResolutionChange(int texResFactor)
    {
        if (timer > .3f)
        {

            textureResolution = (TextureResolutionFactor)texResFactor;
            SetGlobalTextureResolution();

        }

    }
    public void TextureResolutionStartup()
    {
        if (playerData.textureResolution == TextureResolutionFactor.low)
        {
            fullResolution.isOn = false;
            halfResolution.isOn = false;
            qtrResolution.isOn = true;
        }

        if (playerData.textureResolution == TextureResolutionFactor.Medium)
        {
            qtrResolution.isOn = false;
            fullResolution.isOn = false;
            halfResolution.isOn = true;
            
        }

        if (playerData.textureResolution == TextureResolutionFactor.High)
        {
            halfResolution.isOn = false;
            qtrResolution.isOn = false;
            fullResolution.isOn = true;
            
        }
    }
   
    public void VSyncStartup()
    {
        if (VSync == true)
        {
            VSyncOff.isOn = false;
            VSyncOn.isOn = true;
        }
        else
        {
            VSyncOn.isOn = false;
            VSyncOff.isOn = true;
        }
    }
    public void VSyncChange(bool isOn)
    {
        if (timer > .3f)
        {
            if (VSyncOn.isOn)
            {
                VSync = isOn;
                SetGlobalVsync();
            }

            else
            {
                VSync = isOn;
                SetGlobalVsync();
            }
        }

    }

    public void AntiAliasingChange(int antiAliFactor)
    {
        if (timer > .3f)
        {

            antiAliasing = (AntiAliasingFactor)antiAliFactor;
            SetGlobalAntiAliasing();

        }

    }
    public void AntiAliasingStartup()
    {
        if (playerData.antiAliasing == AntiAliasingFactor.None)
        {
            MSAAx2.isOn = false;
            MSAAx4.isOn = false;
            MSAAx8.isOn = false;
            MSAAx16.isOn = false;
            AAOff.isOn = true;
        }

        if (playerData.antiAliasing == AntiAliasingFactor.MSAAx2)
        {
            AAOff.isOn = false;
            MSAAx4.isOn = false;
            MSAAx8.isOn = false;
            MSAAx16.isOn = false;
            MSAAx2.isOn = true;
        }

        if (playerData.antiAliasing == AntiAliasingFactor.MSAAx4)
        {
            AAOff.isOn = false;
            MSAAx2.isOn = false;
            MSAAx8.isOn = false;
            MSAAx16.isOn = false;
            MSAAx4.isOn = true;
        }

        if (playerData.antiAliasing == AntiAliasingFactor.MSAAx8)
        {
            AAOff.isOn = false;
            MSAAx4.isOn = false;
            MSAAx2.isOn = false;
            MSAAx16.isOn = false;
            MSAAx8.isOn = true;
        }

        if (playerData.antiAliasing == AntiAliasingFactor.MSAAx16)
        {
            AAOff.isOn = false;
            MSAAx4.isOn = false;
            MSAAx8.isOn = false;
            MSAAx2.isOn = false;
            MSAAx16.isOn = true;
        }
    }

    public void AmbientOcclusionChange(bool isOn)
    {
        if (timer > .3f)
        {
            if (ambientOcclusionOn.isOn)
            {
                ambientOcclusion = isOn;
                SetGlobalAmbientOcclusion();
            }

            else
            {
                ambientOcclusion = isOn;
                SetGlobalAmbientOcclusion();
            }
        }

    }
    public void AmbientOcclusionStartUp()
    {
        if (ambientOcclusion == true)
        {
            ambientOcclusionOff.isOn = false;
            ambientOcclusionOn.isOn = true;
        }
        else
        {
            ambientOcclusionOn.isOn = false;
            ambientOcclusionOff.isOn = true;
        }
    }
}
