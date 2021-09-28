using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bm.Lerp
{
    public class BmLerpSpline : BmLerpBase
    {
        [HideInInspector]
        public List<Vector3> points = new List<Vector3>();
        public Transform target;
        protected override void _Lerp(float _per)
        {
            target.position = MathTools.GetSplinePoint(points, _per);
        }

        private void Reset()
        {
            target = transform;
            points.Add(Vector3.zero);
            points.Add(Vector3.zero);
        }

    }

}