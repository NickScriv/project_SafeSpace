using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace EZCameraShake
{
    [AddComponentMenu("EZ Camera Shake/Camera Shaker")]
    public class CameraShaker : MonoBehaviour
    {
        /// <summary>
        /// The single instance of the CameraShaker in the current scene. Do not use if you have multiple instances.
        /// </summary>
        public static CameraShaker Instance;
        static Dictionary<string, CameraShaker> instanceList = new Dictionary<string, CameraShaker>();



        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }


        public IEnumerator CameraShake(float duration, float mag)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0.0f;

            while(elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * mag;
                float y = Random.Range(-1f, 1f) * mag;

                transform.localPosition = new Vector3(x, y, originalPos.z);
                elapsed += Time.deltaTime;

                yield return null;

            }
        }

     
    }
}