using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HotteokSurvivor
{
    public class Money : MonoBehaviour
    {
        private GridManager gridManager;
        private Vector2Int currentPos;

        public float hitRadius = 1f;

        public int exp;

        private void OnEnable()
        {
            gridManager = GridManager.Instance;
            currentPos = gridManager.GetCellIndex(transform.position);

            gridManager.AddToCell(currentPos, gameObject);
        }

        private void OnDisable()
        {
            gridManager.DeleteToCell(currentPos, gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }
    }
}