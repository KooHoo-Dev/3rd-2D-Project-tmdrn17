using System.Collections.Generic;
using HotteokSurvivor.Script.Manager_And_System;
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

        public void InIt()
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