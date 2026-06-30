using UnityEngine;
using System.Collections.Generic;
using Study.Utilities;

namespace HotteokSurvivor
{
    public class GridManager : SingletonBase<GridManager>
    {
        
        // 격자 한 칸의 크기 (예: 5x5 단위로 방을 나눔)
        public float cellSize = 5f;

        // '격자 좌표(방 번호)'를 키로 하고, 그 방에 있는 '오브젝트들의 명단'을 값으로 가집니다.
        public Dictionary<Vector2Int, List<GameObject>> grid = new Dictionary<Vector2Int, List<GameObject>>();

        public void AddToCell(Vector2Int cellIndex, GameObject cellObject)
        {
            // 1. 해당 방(cellIndex)에 이미 만들어진 명단(List)이 있는지 확인합니다.
            // TryGetValue는 키가 존재하는지 확인하고, 있다면 값을 out 매개변수로 뽑아주는 아주 빠르고 효율적인 함수입니다.
            if (grid.TryGetValue(cellIndex, out List<GameObject> cellObjects))
            {
                // 2-A. 이미 명단이 존재한다면, 기존 명단에 이 오브젝트를 추가하기만 합니다.
                cellObjects.Add(cellObject);
            }
            else
            {
                // 2-B. 명단이 없다면(이 방에 처음 들어왔다면), 새 명단을 만들고 등록합니다.
                List<GameObject> newcellObjects = new List<GameObject>();
                newcellObjects.Add(cellObject);
                grid.Add(cellIndex, newcellObjects);
            }
        }

        public void DeleteToCell(Vector2Int cellIndex, GameObject cellObject)
        {
            List<GameObject> enemies = grid[cellIndex];

            if (enemies.Count > 0)
            {
                enemies.Remove(cellObject);
            }
            else
            {
                enemies.Remove(cellObject);
                grid.Remove(cellIndex);
            }
        }

        public Vector2Int GetCellIndex(Vector3 position)
        {
            int x = Mathf.FloorToInt(position.x / cellSize);
            int y = Mathf.FloorToInt(position.y / cellSize);
            return new Vector2Int(x, y);
        }
    }
}