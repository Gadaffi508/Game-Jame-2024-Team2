using System;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light[] lights;
    
    private void OnEnable()
    {
        PlayerOxygenGather.lights += ChangeLight;
    }

    private void OnDisable()
    {
        PlayerOxygenGather.lights -= ChangeLight;
    }

    public void ChangeLight()
    {
        foreach (var light in lights)
        {
            light.color=Color.green;
        }
    } 
}
