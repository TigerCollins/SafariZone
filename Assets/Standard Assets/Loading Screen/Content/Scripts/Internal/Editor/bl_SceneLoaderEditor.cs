using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Lovatto.SceneLoader;
using UnityEngine.UI;

[CustomEditor(typeof(bl_SceneLoader))]
public class bl_SceneLoaderEditor : Editor
{
    private bl_SceneLoader Script;

    private void OnEnable()
    {
        Script = (bl_SceneLoader)target;
    }

    public override void OnInspectorGUI()
    {
       if(Script == null) { Script = (bl_SceneLoader)target; }

        GUILayout.BeginVertical("box");

        GUILayout.BeginVertical("box");
        GUILayout.Label("Settings", EditorStyles.helpBox);
        GUILayout.Space(2);
        Script.SkipType = (SceneSkipType)EditorGUILayout.EnumPopup("Skip Type", Script.SkipType, EditorStyles.toolbarDropDown);
        GUILayout.Space(2);
        Script.SceneSmoothLoad = EditorGUILayout.Slider("Progress Smoothing", Script.SceneSmoothLoad, 0.5f, 7);
        Script.FadeInSpeed = EditorGUILayout.Slider("FadeIn Speed", Script.FadeInSpeed, 0.5f, 7);
        Script.FadeOutSpeed = EditorGUILayout.Slider("FadeOut Speed", Script.FadeOutSpeed, 0.5f, 7);
        Script.useTimeScale = EditorGUILayout.ToggleLeft("Use Time Scale", Script.useTimeScale, EditorStyles.toolbarButton);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Background", EditorStyles.helpBox);
        GUILayout.Space(2);
        Script.useBackgrounds = EditorGUILayout.ToggleLeft("use Backgrounds", Script.useBackgrounds, EditorStyles.toolbarButton);
        GUILayout.Space(2);
        if (Script.useBackgrounds)
        {
            Script.TimePerBackground = EditorGUILayout.Slider("Time Per Background", Script.TimePerBackground, 1, 30);
            Script.TimeBetweenBackground = EditorGUILayout.Slider("Time Between Backgrounds", Script.TimeBetweenBackground, 0.5f, 5);
            Script.FadeBackgroundSpeed = EditorGUILayout.Slider("Fade Background Speed", Script.FadeBackgroundSpeed, 0.5f, 7);
        }
        Script.ShowDescription = EditorGUILayout.ToggleLeft("Show scene description", Script.ShowDescription, EditorStyles.toolbarButton);
        GUILayout.Space(2);
        Script.StartFadeInCurve = EditorGUILayout.CurveField("Start FadeIn Curve", Script.StartFadeInCurve);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Tips", EditorStyles.helpBox);
        GUILayout.Space(2);
        Script.RandomTips = EditorGUILayout.ToggleLeft("Show Random Tips", Script.RandomTips, EditorStyles.toolbarButton);
        GUILayout.Space(2);
        if (Script.RandomTips)
        {
            Script.TimePerTip = EditorGUILayout.Slider("Time Per Tip", Script.TimePerTip, 1, 30);
            Script.FadeTipsSpeed = EditorGUILayout.Slider("Fade Tip UI Speed", Script.FadeTipsSpeed, 0.5f, 5);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Progress", EditorStyles.helpBox);
        GUILayout.Space(2);
        Script.FadeLoadingBarOnFinish = EditorGUILayout.ToggleLeft("Hide Loading bar on finish", Script.FadeLoadingBarOnFinish, EditorStyles.toolbarButton);
        GUILayout.Space(2);
        if (Script.LoadingCircle != null)
        {
            Script.LoadingCircleSpeed = EditorGUILayout.Slider("Loading Circle Speed", Script.LoadingCircleSpeed, 50, 1000);
            Script.LoadingTextFormat = EditorGUILayout.TextField("Loading Text Format", Script.LoadingTextFormat);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Audio", EditorStyles.helpBox);
        GUILayout.Space(2);
        Script.AudioVolume = EditorGUILayout.Slider("Audio Volume", Script.AudioVolume, 0.01f, 1);
        Script.FadeAudioSpeed = EditorGUILayout.Slider("Fade Audio Speed", Script.FadeAudioSpeed, 0.5f, 5);
        Script.FinishSoundVolume = EditorGUILayout.Slider("Finish Sound Volume", Script.FinishSoundVolume, 0.01f, 1);
        Script.BackgroundAudio = EditorGUILayout.ObjectField("Background Audio", Script.BackgroundAudio, typeof(AudioClip), false) as AudioClip;
        Script.FinishSound = EditorGUILayout.ObjectField("Finish Sound", Script.FinishSound, typeof(AudioClip), false) as AudioClip;
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Label("References", EditorStyles.helpBox);
        GUILayout.Space(2);
        GUILayout.BeginHorizontal();
        GUILayout.Space(14);
        Script._showReferences = EditorGUILayout.Foldout(Script._showReferences,"References");
        GUILayout.EndHorizontal();
        if (Script._showReferences)
        {
            Script.SceneNameText = EditorGUILayout.ObjectField("SceneNameText", Script.SceneNameText, typeof(Text), true) as Text;
            Script.DescriptionText = EditorGUILayout.ObjectField("DescriptionText", Script.DescriptionText, typeof(Text), true) as Text;
            Script.ProgressText = EditorGUILayout.ObjectField("ProgressText", Script.ProgressText, typeof(Text), true) as Text;
            Script.BackgroundImage = EditorGUILayout.ObjectField("BackgroundImage", Script.BackgroundImage, typeof(Image), true) as Image;
            Script.FilledImage = EditorGUILayout.ObjectField("FilledImage", Script.FilledImage, typeof(Image), true) as Image;
            Script.LoadBarSlider = EditorGUILayout.ObjectField("LoadBarSlider", Script.LoadBarSlider, typeof(Slider), true) as Slider;
            Script.ContinueUI = EditorGUILayout.ObjectField("ContinueUI", Script.ContinueUI, typeof(GameObject), true) as GameObject;
            Script.RootUI = EditorGUILayout.ObjectField("RootUI", Script.RootUI, typeof(GameObject), true) as GameObject;
            Script.FlashImage = EditorGUILayout.ObjectField("FlashImage", Script.FlashImage, typeof(GameObject), true) as GameObject;
            Script.SkipKeyText = EditorGUILayout.ObjectField("SkipKeyText", Script.SkipKeyText, typeof(GameObject), true) as GameObject;
            Script.LoadingCircle = EditorGUILayout.ObjectField("LoadingCircle", Script.LoadingCircle, typeof(RectTransform), true) as RectTransform;
            Script.LoadingCircleCanvas = EditorGUILayout.ObjectField("LoadingCircleCanvas", Script.LoadingCircleCanvas, typeof(CanvasGroup), true) as CanvasGroup;
            Script.FadeImageCanvas = EditorGUILayout.ObjectField("FadeImageCanvas", Script.FadeImageCanvas, typeof(CanvasGroup), true) as CanvasGroup;
        }
        GUILayout.EndVertical();

        GUILayout.EndVertical();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }
    }
}