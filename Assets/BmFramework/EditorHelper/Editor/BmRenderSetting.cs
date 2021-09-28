using System;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

public class BmRenderSetting : UnityEngine.Object
{
   
    public  float ambientSkyboxAmount { get; set; }
    //
    // 摘要:
    //     Size of the Light halos.
    public  float haloStrength { get; set; }
    //
    // 摘要:
    //     Cubemap resolution for default reflection.
    public  int defaultReflectionResolution { get; set; }
    //
    // 摘要:
    //     Default reflection mode.
    public  DefaultReflectionMode defaultReflectionMode { get; set; }
    //
    // 摘要:
    //     The number of times a reflection includes other reflections.
    public  int reflectionBounces { get; set; }
    //
    // 摘要:
    //     How much the skybox / custom cubemap reflection affects the Scene.
    public  float reflectionIntensity { get; set; }
    //
    // 摘要:
    //     Custom specular reflection cubemap.
    public  Cubemap customReflection { get; set; }
    //
    // 摘要:
    //     Custom or skybox ambient lighting data.
    public  SphericalHarmonicsL2 ambientProbe { get; set; }
    //
    // 摘要:
    //     The light used by the procedural skybox.
    public  Light sun { get; set; }

    public  Material skybox { get; set; }
    //
    // 摘要:
    //     The color used for the sun shadows in the Subtractive lightmode.
    public  Color subtractiveShadowColor { get; set; }
    //
    // 摘要:
    //     The intensity of all flares in the Scene.
    public  float flareStrength { get; set; }

    public  Color ambientLight { get; set; }
    //
    // 摘要:
    //     Ambient lighting coming from below.
    public  Color ambientGroundColor { get; set; }
    //
    // 摘要:
    //     Ambient lighting coming from the sides.
    public  Color ambientEquatorColor { get; set; }
    //
    // 摘要:
    //     Ambient lighting coming from above.
    public  Color ambientSkyColor { get; set; }
    //
    // 摘要:
    //     Ambient lighting mode.
    public  AmbientMode ambientMode { get; set; }
    //
    // 摘要:
    //     The density of the exponential fog.
    public  float fogDensity { get; set; }
    //
    // 摘要:
    //     The color of the fog.
    public  Color fogColor { get; set; }
    //
    // 摘要:
    //     Fog mode to use.
    public  FogMode fogMode { get; set; }
  
    public  float fogEndDistance { get; set; }

    public  float fogStartDistance { get; set; }
 
    public  bool fog { get; set; }
    //
    // 摘要:
    //     How much the light from the Ambient Source affects the Scene.
    public  float ambientIntensity { get; set; }
    //
    // 摘要:
    //     The fade speed of all flares in the Scene.
    public  float flareFadeSpeed { get; set; }
}
