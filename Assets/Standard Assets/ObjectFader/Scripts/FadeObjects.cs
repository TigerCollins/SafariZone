using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeObjects : MonoBehaviour {
    enum CastType
    {
        ray,
        sphere,
        capsule,
    }

    [SerializeField]
    private CastType castType;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    bool includeTriggers;
    [SerializeField]
    [Range(0.01f, 10.0f)]
    private float radius = 1;
    [SerializeField]
    [Range(0.01f, 10.0f)]
    private float capsuleHeight = 1;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float maxTransparency = 0.5f;
    [SerializeField]
    [Range(0.01f, 5.0f)]
    private float fadeSpeed = 1f;
    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float depthTolerance = 0.5f;
    private QueryTriggerInteraction triggerSetting;
    private RaycastHit[] castHits;
    private List<RaycastHit> activehits = new List<RaycastHit>();
    private float rayDistance;

	void Update ()
    {
        rayDistance =  Vector3.Distance(player.transform.position, transform.position);
        if (includeTriggers)
        {
            triggerSetting = QueryTriggerInteraction.Collide;
        }
        else
        {
            triggerSetting = QueryTriggerInteraction.Ignore;
        }

        if (castType == CastType.ray)
        {
            castHits = Physics.RaycastAll(transform.position, Vector3.Normalize(player.transform.position - transform.position), rayDistance, Physics.DefaultRaycastLayers, triggerSetting);
        }
        else if (castType == CastType.sphere)
        {
            castHits = Physics.SphereCastAll(transform.position, radius, Vector3.Normalize(player.transform.position - transform.position), rayDistance, Physics.DefaultRaycastLayers, triggerSetting);
        }
        else if (castType == CastType.capsule)
        {
            castHits = Physics.CapsuleCastAll(new Vector3 (transform.position.x, transform.position.y - capsuleHeight * 0.5f, transform.position.z), new Vector3(transform.position.x, transform.position.y + capsuleHeight * 0.5f, transform.position.z), radius, Vector3.Normalize(player.transform.position - transform.position), rayDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide);
        }
        for (int i = 0; i < castHits.Length; i++)
        {
            RaycastHit hit = castHits[i];

            float objectDistance = Vector3.Distance(transform.position, hit.transform.position);
            if (objectDistance + depthTolerance > rayDistance)
            {
                continue;
            }

            Renderer rend = hit.transform.GetComponent<Renderer>();
            bool canFade = false;
            Transform t = hit.transform.parent;
            if (hit.transform.tag == "CanFade")
            {
                canFade = true;
            }
            else
            {
                while (t != null)
                {
                    if (hit.transform.parent.tag == "CanFade")
                    {
                        canFade = true;
                    }
                    t = t.parent;
                }
            }
            if ((rend) && canFade)
            {
                bool isNew = true;
                foreach(RaycastHit r in activehits)
                {
                    if (r.transform == hit.transform)
                    {
                        isNew = false;
                    }
                }
                if (isNew)
                {
                    activehits.Add(hit);
                    StartCoroutine(FadeMaterial (rend, hit));
                } 
            }
        }
    }

    IEnumerator FadeMaterial(Renderer rend, RaycastHit hit)
    {
        string colorPropertyName = "_Color";
        Material[] sharedMaterials = rend.sharedMaterials;
        Material[] fadeMaterials = new Material[sharedMaterials.Length];
        float[] startAlphas = new float[sharedMaterials.Length];
        for (int i = 0; i < sharedMaterials.Length; i++)
        {
            if (hit.transform.GetComponent<FadeObjectOverride>())
            {
                if (hit.transform.GetComponent<FadeObjectOverride>().fadeShader)
                {
                    fadeMaterials[i] = new Material(hit.transform.GetComponent<FadeObjectOverride>().fadeShader);
                }
                else
                {
                    fadeMaterials[i] = new Material(sharedMaterials[i].shader);
                }
                fadeMaterials[i].CopyPropertiesFromMaterial(sharedMaterials[i]);
                colorPropertyName = hit.transform.GetComponent<FadeObjectOverride>().alphaPropertyName;
                if (fadeMaterials[i].GetFloat("_Mode") == 0 || fadeMaterials[i].GetFloat("_Mode") == 1)
                {
                    SetMaterialProperties(fadeMaterials[i], 2);
                }
            }
            else
            {
                fadeMaterials[i] = new Material(sharedMaterials[i].shader); 
                fadeMaterials[i].CopyPropertiesFromMaterial(sharedMaterials[i]);
                if (fadeMaterials[i].GetFloat("_Mode") == 0 || fadeMaterials[i].GetFloat("_Mode") == 1)
                {
                    SetMaterialProperties(fadeMaterials[i], 2);
                }
            }
            startAlphas[i] = fadeMaterials[i].GetColor(colorPropertyName).a;
        }
        rend.materials = fadeMaterials;
        float t = 0;
        while (t < 1 / fadeSpeed)
        {
            t += Time.deltaTime;
            for (int i = 0; i < fadeMaterials.Length; i++)
            {
                fadeMaterials[i].SetColor(colorPropertyName, new Color(fadeMaterials[i].GetColor(colorPropertyName).r, fadeMaterials[i].GetColor(colorPropertyName).g, fadeMaterials[i].GetColor(colorPropertyName).b, Mathf.Clamp(fadeMaterials[i].GetColor(colorPropertyName).a - Time.deltaTime * fadeSpeed, maxTransparency, 1)));
            }
            yield return null;
        }
        bool isBlocking = true;
        while(isBlocking)
        {
            isBlocking = false;
            foreach (RaycastHit blockHit in castHits)
            {
                if(blockHit.transform == hit.transform)
                {
                    isBlocking = true;
                }
            }
            yield return null;
        }
        t = 0;
        while (t < 1 / fadeSpeed)
        {
            t += Time.deltaTime;
            for (int i = 0; i < fadeMaterials.Length; i++)
            {
                fadeMaterials[i].SetColor(colorPropertyName, new Color(fadeMaterials[i].GetColor(colorPropertyName).r, fadeMaterials[i].GetColor(colorPropertyName).g, fadeMaterials[i].GetColor(colorPropertyName).b, Mathf.Clamp(fadeMaterials[i].GetColor(colorPropertyName).a + Time.deltaTime * fadeSpeed, maxTransparency, startAlphas[i])));
            }
            yield return null;
        }
        for (int i = 0; i < sharedMaterials.Length; i++)
        {
            Material.Destroy(fadeMaterials[i]);
        }
        rend.materials = sharedMaterials;
        Array.Clear(sharedMaterials, 0, sharedMaterials.Length);
        Array.Clear(fadeMaterials, 0, fadeMaterials.Length);
        Array.Clear(startAlphas, 0, startAlphas.Length);
        activehits.Remove(hit);
    }
   

    void SetMaterialProperties(Material material, int blendMode)
    {
        switch (blendMode)
        {
            case 0:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case 1:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case 2:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case 3:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
