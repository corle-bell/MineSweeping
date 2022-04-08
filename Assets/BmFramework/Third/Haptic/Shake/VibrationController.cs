using System;
using UnityEngine;
using MoreMountains.NiceVibrations;

namespace BmFramework.Core
{
    public class VibrationController : MonoBehaviour
    {
        public static VibrationController instance { get; private set; }

        public bool vibrate { get; set; }

        public float delay = 0.15f;

        private float timestamp = -10;
        private void Awake()
        {
            instance = this;
            vibrate = true;
            MMVibrationManager.iOSInitializeHaptics();
        }

        private void OnDestroy()
        {
			MMVibrationManager.iOSReleaseHaptics();
        }

        public void Impact(HapticTypes _type= HapticTypes.LightImpact)
        {
            if (!vibrate) { return; }
            if (Time.realtimeSinceStartup - timestamp < delay) return;

            timestamp = Time.realtimeSinceStartup;
            MMVibrationManager.Haptic(_type);

#if UNITY_EDITOR
            //Debug.Log("Impact:");
#endif
        }
    }
}
