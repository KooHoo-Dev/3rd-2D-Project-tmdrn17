using System.Collections.Generic;
using UnityEngine;

namespace HotteokSurvivor
{
    public class HotteokRotate : MonoBehaviour
    {
        public float rotateSpeed = 1.0f;

        public Dictionary<string, List<UnityEngine.GameObject>> rotateHotteoks = new Dictionary<string, List<UnityEngine.GameObject>>();

        public void AddHotteok(string name, UnityEngine.GameObject hotteok)
        {
            if (rotateHotteoks.TryGetValue(name, out List<UnityEngine.GameObject> hotteoks))
            {
                hotteoks.Add(hotteok);
            }
            else
            {
                List<UnityEngine.GameObject> newHotteoks = new List<UnityEngine.GameObject>();
                newHotteoks.Add(hotteok);
                rotateHotteoks.Add(name, newHotteoks);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}
