using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace HotteokSurvivor
{
    public abstract class Hotteok : MonoBehaviour
    {
        [SerializeField] protected Enemy enemy;
        [SerializeField] protected Transform startPos;
        [SerializeField] private float endDistance;
        [SerializeField] private float shotSpeed;
        [SerializeField] private float debugRadius;

        protected virtual void Start()
        {
            transform.position = startPos.position;
            gameObject.SetActive(true);
        }

        protected void Update()
        {
            float distance = Vector2.Distance(startPos.position, transform.position);

            RaycastHit2D hit = Physics2D.CircleCast
                (transform.position, debugRadius, Vector3.zero);

            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log($"{hit.collider.gameObject.name}");
            }

/*            if (distance >= endDistance)
            {
                transform.position = startPos.position;
                gameObject.SetActive(false);
            }*/
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, debugRadius);
        }
    }
}