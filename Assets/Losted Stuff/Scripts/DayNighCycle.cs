using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNighCycle : MonoBehaviour
{
    public Material dayMaterial;
    public Material nightMaterial;
    float duration = 10000.0f;

    private void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.Lerp(nightMaterial, dayMaterial, lerp);
    }
}
