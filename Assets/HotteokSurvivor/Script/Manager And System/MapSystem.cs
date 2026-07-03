using UnityEngine;

namespace HotteokSurvivor
{
    public class MapSystem : MonoBehaviour
    {
        [SerializeField] private Transform player;      // 플레이어의 Transform [1, 2]
        [SerializeField] private Transform[] mapTiles;  // 9개의 맵 타일 배열 (아파트 우편함 구조) [3]
        [SerializeField] private float tileSizeX;   // 타일 한 칸의 가로/세로 크기 (월드 단위)
        [SerializeField] private float tileSizeY;   // 타일 한 칸의 가로/세로 크기 (월드 단위)
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            tileSizeX = spriteRenderer.bounds.size.x;
            tileSizeY = spriteRenderer.bounds.size.y;
        }

        private void Update()
        {
            // 9개의 타일을 매 프레임 순회하며 검사합니다 [4]
            for (int i = 0; i < mapTiles.Length; i++)
            {
                Vector3 tilePos = mapTiles[i].position; // [2, 5]
                Vector3 playerPos = player.position;

                // 플레이어와 타일 사이의 거리(방향)를 구합니다 (목표 - 나) [6, 7]
                float diffX = playerPos.x - tilePos.x;
                float diffY = playerPos.y - tilePos.y;

                // X축, Y축 거리를 독립적으로 판정하기 위해 절댓값을 구합니다 [8-10]
                float absDiffX = Mathf.Abs(diffX);
                float absDiffY = Mathf.Abs(diffY);

                // 1. X축 이동 판정 (플레이어가 타일 경계선(1.5배 거리)을 넘었을 때)
                if (absDiffX > tileSizeX * 1.5f)
                {
                    // 플레이어가 타일보다 오른쪽에 있다면 +1, 왼쪽에 있다면 -1을 반환합니다 [10]
                    float dirX = Mathf.Sign(diffX);

                    // 타일을 반대편 끝(타일 3장 너비만큼)으로 순간 이동시킵니다 [11, 12]
                    mapTiles[i].position += Vector3.right * dirX * tileSizeX * 3f;
                }

                // 2. Y축 이동 판정
                if (absDiffY > tileSizeY * 1.5f)
                {
                    float dirY = Mathf.Sign(diffY);
                    mapTiles[i].position += Vector3.up * dirY * tileSizeY * 3f;
                }
            }
        }
    }
}