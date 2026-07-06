using System;
using Unity.VisualScripting;
using UnityEngine;


namespace HotteokSurvivor
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float shotSpeed = 1.0f;

        [SerializeField] private Player target;

        private Vector3 goalPosition;

        private void Start()
        {
            target = transform.parent.gameObject.GetComponent<Enemy>().Target;

            goalPosition = target.transform.position;
            Vector3 myPosition = transform.position;
            Vector3 dir = (goalPosition - myPosition).normalized;
            transform.up = dir;
        }

        private void Update()
        {
            transform.Translate(Vector3.up * shotSpeed * Time.deltaTime);
        }

        public void Shoot()
        {

        }
    }
}