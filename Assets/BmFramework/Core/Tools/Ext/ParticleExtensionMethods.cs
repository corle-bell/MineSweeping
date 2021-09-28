using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleExtensionMethods
{
    public static void ChangeColor(this ParticleSystem particle, Color color)
    {
        ParticleSystem.MainModule main = particle.main;
        ParticleSystem.MinMaxGradient startColor = main.startColor;
        startColor.color = color;
        main.startColor = startColor;
    }

    public static void ChangeColor(this ParticleSystem particle, Color min, Color max)
    {
        ParticleSystem.MainModule main = particle.main;
        ParticleSystem.MinMaxGradient startColor = main.startColor;
        startColor.colorMin = min;
        startColor.colorMax = max;
        main.startColor = startColor;
    }
}
