using System;
using UnityEngine;


namespace HotteokSurvivor
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.0f;
        [SerializeField] private float shotSpeed = 1.0f;

        private void Update()
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            transform.position+=
        }

        public void Shoot()
        {

        }
    }
}