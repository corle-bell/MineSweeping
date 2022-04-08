using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BmFramework.Core.Rendering
{
    public class GPUInstance_Painter_Position_Color : GPUInstance_Painter
    {
        private ComputeBuffer positionBuffer;
        private ComputeBuffer colorBuffer;
      

        Vector3[] positions;
        Vector4[] colors;

        protected override void OnBufferInit(IGPUInstance_Object[] objs)
        {
            positions = new Vector3[instanceCount];
            colors = new Vector4[instanceCount];

            colorBuffer = new ComputeBuffer(instanceCount, 16);
            positionBuffer = new ComputeBuffer(instanceCount, 12);
        }

        protected override void OnBufferUpdate()
        {
            UpdatePositionBuffer();
            UpdateColorBuffer();
        }

        protected override void OnBufferRelease()
        {
            if (positionBuffer != null)
                positionBuffer.Release();
            positionBuffer = null;

            if (colorBuffer != null)
                colorBuffer.Release();
            colorBuffer = null;
        }


        private void UpdatePositionBuffer()
        {

            for (int i = 0; i < instanceCount; i++)
            {
                positions[i] = renderObj[i].position;
            }

            positionBuffer.SetData(positions);
            instanceMaterial.SetBuffer("positionBuffer", positionBuffer);
        }

        private void UpdateColorBuffer()
        {
            for (int i = 0; i < instanceCount; i++)
            {
                colors[i] = renderObj[i].color;
            }
            colorBuffer.SetData(colors);
            instanceMaterial.SetBuffer("colorBuffer", colorBuffer);
        }

    }  
}

