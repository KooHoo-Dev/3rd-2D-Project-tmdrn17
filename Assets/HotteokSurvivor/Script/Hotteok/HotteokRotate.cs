using System.Collections.Generic;
using UnityEngine;

namespace HotteokSurvivor
{
    public class HotteokRotate : MonoBehaviour
    {
        public float rotateSpeed = 1.0f;

        [SerializeField] private List<GameObject> ddalgiHottoeks = new List<GameObject>();

        public void AddHotteok(int number)
        {
            ddalgiHottoeks[number].SetActive(true);
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}
