using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BmFramework.Core.Rendering
{
    public class GPUInstance_Painter_Transform_Color : GPUInstance_Painter
    {
        
        private ComputeBuffer positionBuffer;
        private ComputeBuffer scaleBuffer;
        private ComputeBuffer rotateBuffer;
        private ComputeBuffer colorBuffer;

        Vector3[] positions;
        Vector3[] scales;
        Vector4[] colors;
        Vector4[] rotations;

        protected override void OnBufferInit(IGPUInstance_Object[] objs)
        {
            positions = new Vector3[instanceCount];
            scales = new Vector3[instanceCount];
            rotations = new Vector4[instanceCount];
            colors = new Vector4[instanceCount];

            colorBuffer = new ComputeBuffer(instanceCount, 16);

            positionBuffer = new ComputeBuffer(instanceCount, 12);
            scaleBuffer = new ComputeBuffer(instanceCount, 12);
            rotateBuffer = new ComputeBuffer(instanceCount, 16);
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

            if (scaleBuffer != null)
                scaleBuffer.Release();
            scaleBuffer = null;

            if (rotateBuffer != null)
                rotateBuffer.Release();
            rotateBuffer = null;

            if (colorBuffer != null)
                colorBuffer.Release();
            colorBuffer = null;
        }

        
        private void UpdatePositionBuffer()
        {

            for (int i = 0; i < instanceCount; i++)
            {
                positions[i] = renderObj[i].position;
                scales[i] = renderObj[i].scale;
                rotations[i] = renderObj[i].rotate;
            }

            positionBuffer.SetData(positions);
            instanceMaterial.SetBuffer("positionBuffer", positionBuffer);

            scaleBuffer.SetData(scales);
            instanceMaterial.SetBuffer("scaleBuffer", scaleBuffer);

            rotateBuffer.SetData(rotations);
            instanceMaterial.SetBuffer("rotationBuffer", rotateBuffer);
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

