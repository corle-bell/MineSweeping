using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BmFramework.Core.Rendering
{
    public class GPUInstance_Painter : MonoBehaviour
    {
        [EnumName("是否接收阴影")]
        public bool isReciveShadow=true;
        [EnumName("阴影")]
        public ShadowCastingMode castingMode = ShadowCastingMode.On;
        [EnumName("网格")]
        public Mesh instanceMesh;
        [EnumName("材质")]
        public Material instanceMaterial;
        [EnumName("边界盒")]
        public Vector3 boundsSize = new Vector3(100, 100, 100);
        [EnumName("实例数量")]
        public int instanceCount;

        public IGPUInstance_Object[] renderObj;

        private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
        private int subMeshIndex;
        private ComputeBuffer argsBuffer;

        private void Awake()
        {
            argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        }

        private void OnDestroy()
        {
            OnBufferRelease();

            if (argsBuffer != null)
                argsBuffer.Release();
            argsBuffer = null;
        }

        void OnEnable()
        {
            GPUInstance.I.AddPainter(this);
        }

        private void OnDisable()
        {
            GPUInstance.I.RemovePainter(this);
        }

        public void Init(IGPUInstance_Object[] objs)
        {
            instanceCount = objs.Length;

            OnBufferInit(objs);

            renderObj = objs;
            argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            UpdateBuffers();
        }

        public void Repaint()
        {
            UpdateBuffers();
        }

        void UpdateBuffers()
        {
            if (instanceMesh != null)
                subMeshIndex = Mathf.Clamp(subMeshIndex, 0, instanceMesh.subMeshCount - 1);

            OnBufferUpdate();

            // Indirect args
            if (instanceMesh != null)
            {
                args[0] = (uint)instanceMesh.GetIndexCount(subMeshIndex);
                args[1] = (uint)instanceCount;
                args[2] = (uint)instanceMesh.GetIndexStart(subMeshIndex);
                args[3] = (uint)instanceMesh.GetBaseVertex(subMeshIndex);
            }
            else
            {
                args[0] = args[1] = args[2] = args[3] = 0;
            }
            argsBuffer.SetData(args);

        }

        public void Logic()
        {
            UpdateBuffers();           
            Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, new Bounds(transform.position, boundsSize), argsBuffer, 0, null, castingMode, isReciveShadow);
        }

        protected virtual void OnBufferInit(IGPUInstance_Object[] objs)
        {

        }

        protected virtual void OnBufferUpdate()
        {

        }

        protected virtual void OnBufferRelease()
        {

        }
    }  
}

