// Date   : 08.11.2018 22:06
// Project: PortableLoopingMachine
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeMaterials : MonoBehaviour {

    bool fading = false;

    private float endValue = 0f;

    [SerializeField]
    private float duration = 1.2f;

    [SerializeField]
    private List<MaterialToFade> materialsToFade;

    private float startTime;

    void Start () {
        foreach (MaterialToFade tofade in materialsToFade)
        {
            tofade.currentValue = tofade.startValue;
            Color targetColor = tofade.material.GetColor(tofade.targetFieldName);
            targetColor.a = tofade.currentValue;
            tofade.material.SetColor(tofade.targetFieldName, targetColor);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    void Update () {
        float time = Time.time;
        foreach (MaterialToFade tofade in materialsToFade)
        {
            float t = (time - startTime) / duration;
            tofade.currentValue = Mathf.SmoothStep(tofade.currentValue, endValue, t);
            Color targetColor = tofade.material.GetColor(tofade.targetFieldName);
            targetColor.a = tofade.currentValue;
            tofade.material.SetColor(tofade.targetFieldName, targetColor);
        }

    }
}

[System.Serializable]
public class MaterialToFade : System.Object
{
    public Material material;
    public float startValue;
    public string targetFieldName;
    [HideInInspector]
    public float currentValue;
}