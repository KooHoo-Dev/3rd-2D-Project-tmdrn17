using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HotteokSurvivor
{
    public class HotteokRotate : MonoBehaviour
    {
        public float RotateSpeed = 1.0f;
        public float ActiveCool = 1.0f;
        public int ActiveHotteoks = 0;

        public float DdalgiHotteokCool = 1.0f;

        [SerializeField] private bool isActive = false;

        public List<GameObject> DdalgiHottoeks = new List<GameObject>();

        public void AddHotteok(int number)
        {
            DdalgiHottoeks[number].SetActive(true);
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);

            if (isActive == false)
            {
                StartCoroutine(ActiveCoroutine());
            }
        }

        private IEnumerator ActiveCoroutine()
        {
            isActive = true;

            for (int i = 0; i < ActiveHotteoks; i++)
            {
                DdalgiHottoeks[i].SetActive(true);
            }

            yield return new WaitForSeconds(ActiveCool);

            for (int i = 0; i < ActiveHotteoks; i++)
            {
                DdalgiHottoeks[i].SetActive(false);
            }

            yield return new WaitForSeconds(DdalgiHotteokCool);

            isActive = false;
        }
    }
}
