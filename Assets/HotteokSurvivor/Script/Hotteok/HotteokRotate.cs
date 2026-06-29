using System.Collections.Generic;
using UnityEngine;

namespace HotteokSurvivor
{
    public class HotteokRotate : MonoBehaviour
    {
        public float rotateSpeed = 1.0f;

        public Dictionary<string, List<GameObject>> rotateHotteoks = new Dictionary<string, List<GameObject>>();

        public void AddHotteok(string name, GameObject hotteok)
        {
            if (rotateHotteoks.TryGetValue(name, out List<GameObject> hotteoks))
            {
                hotteoks.Add(hotteok);
            }
            else
            {
                List<GameObject> newHotteoks = new List<GameObject>();
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
