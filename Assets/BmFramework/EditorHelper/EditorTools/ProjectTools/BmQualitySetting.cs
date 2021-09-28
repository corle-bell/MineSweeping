using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BmQualitySetting : ScriptableObject
{
    public  int particleRaycastBudget;
    //
    // 摘要:
    //     Use a two-pass shader for the vegetation in the terrain engine.
    public  bool softVegetation;
    //
    // 摘要:
    //     The VSync Count.
    public  int vSyncCount;
    //
    // 摘要:
    //     Set The AA Filtering option.
    public  int antiAliasing;
    //
    // 摘要:
    //     Async texture upload provides timesliced async texture upload on the render thread
    //     with tight control over memory and timeslicing. There are no allocations except
    //     for the ones which driver has to do. To read data and upload texture data a ringbuffer
    //     whose size can be controlled is re-used. Use asyncUploadTimeSlice to set the
    //     time-slice in milliseconds for asynchronous texture uploads per frame. Minimum
    //     value is 1 and maximum is 33.
    public  int asyncUploadTimeSlice;
    //
    // 摘要:
    //     Async texture upload provides timesliced async texture upload on the render thread
    //     with tight control over memory and timeslicing. There are no allocations except
    //     for the ones which driver has to do. To read data and upload texture data a ringbuffer
    //     whose size can be controlled is re-used. Use asyncUploadBufferSize to set the
    //     buffer size for asynchronous texture uploads. The size is in megabytes. Minimum
    //     value is 2 and maximum is 512. Although the buffer will resize automatically
    //     to fit the largest texture currently loading, it is recommended to set the value
    //     approximately to the size of biggest texture used in the Scene to avoid re-sizing
    //     of the buffer which can incur performance cost.
    public  int asyncUploadBufferSize;
    //
    // 摘要:
    //     This flag controls if the async upload pipeline's ring buffer remains allocated
    //     when there are no active loading operations. Set this to true, to make the ring
    //     buffer allocation persist after all upload operations have completed. If you
    //     have issues with excessive memory usage, you can set this to false. This means
    //     you reduce the runtime memory footprint, but memory fragmentation can occur.
    //     The default value is true.
    public  bool asyncUploadPersistentBuffer;
    //
    // 摘要:
    //     Enables realtime reflection probes.
    public  bool realtimeReflectionProbes;
    //
    // 摘要:
    //     If enabled, billboards will face towards camera position rather than camera orientation.
    public  bool billboardsFaceCameraPosition;
    //
    // 摘要:
    //     In resolution scaling mode, this factor is used to multiply with the target Fixed
    //     DPI specified to get the actual Fixed DPI to use for this quality setting.
    public  float resolutionScalingFixedDPIFactor;
    //
    // 摘要:
    //     Should soft blending be used for particles?
    public  bool softParticles;
 
    //
    // 摘要:
    //     Skin weights.
    public  SkinWeights skinWeights;
    //
    // 摘要:
    //     Enable automatic streaming of texture mipmap levels based on their distance from
    //     all active cameras.
    public  bool streamingMipmapsActive;
    //
    // 摘要:
    //     The total amount of memory to be used by streaming and non-streaming textures.
    public  float streamingMipmapsMemoryBudget;
    //
    // 摘要:
    //     Number of renderers used to process each frame during the calculation of desired
    //     mipmap levels for the associated textures.
    public  int streamingMipmapsRenderersPerFrame;
    //
    // 摘要:
    //     The maximum number of mipmap levels to discard for each texture.
    public  int streamingMipmapsMaxLevelReduction;
    //
    // 摘要:
    //     Process all enabled Cameras for texture streaming (rather than just those with
    //     StreamingController components).
    public  bool streamingMipmapsAddAllCameras;
    //
    // 摘要:
    //     The maximum number of active texture file IO requests from the texture streaming
    //     system.
    public  int streamingMipmapsMaxFileIORequests;
  
    public  int maxQueuedFrames;


    public  int masterTextureLimit;
    //
    // 摘要:
    //     The maximum number of pixel lights that should affect any object.
    public  int pixelLightCount;
    //
    // 摘要:
    //     A maximum LOD level. All LOD groups.
    public  int maximumLODLevel;

    public  ShadowProjection shadowProjection;

    public  int shadowCascades;

    public  float shadowDistance;

    public  ShadowQuality shadows;

    public  ShadowmaskMode shadowmaskMode;

    public  float shadowNearPlaneOffset;

    public  float shadowCascade2Split;

    public  Vector3 shadowCascade4Split;

    public  float lodBias;
   
    public  AnisotropicFiltering anisotropicFiltering;
   
    public  ShadowResolution shadowResolution;
}
