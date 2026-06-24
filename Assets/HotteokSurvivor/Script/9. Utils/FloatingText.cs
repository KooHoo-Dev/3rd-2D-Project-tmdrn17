using UnityEngine;
using TMPro;
using System.Collections;

namespace Study.Utilities
{
    // # FloatingText:
    // - 미디어나 게임개발, 모바일 앱 환경 등에서 화면 위에 떠다니거나
    //   둥둥 떠오르는 듯한 텍스트 효과나 위젯등을 의미

    // 개발 목표
    // - FloatingText의 특정 함수를 이용해서 위로 올라가는 연출과 사라지는 듯한
    //   효과를 주고자 한다.

    public class FloatingText : MonoBehaviour
    {
        [field: SerializeField] public float VerticalSpeed { get; private set; } = 1.0f;
        [field: SerializeField] public float LifeTime { get; private set; } = 2.0f;

        private TMP_Text text;
        private Color originalColor;
        private Renderer textRenderer;
        private float progressTime = 0.0f;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            textRenderer = GetComponent<Renderer>();
            
            // 렌더링 순서 정렬인데 3차원 렌더러여서
            // orderInLayer가 아닌 soringOrder를 사용
            // 원리는 같은데 3차원에서 사용되는 함수
            textRenderer.sortingOrder = 1000;

            originalColor = text.color;
        }

        public void Show(string message, Color color, Vector3 worldPosition)
        {
            text.SetText(message);
            originalColor = color;
            transform.position = worldPosition;

            StartCoroutine(AnimateCoroutine());
        }

        private IEnumerator AnimateCoroutine()
        {
            progressTime = 0.0f;

            // 원본 색상을 복사해서 알파값만 0인 색상을 만든다
            Color targetColor = originalColor;
            targetColor.a = 0.0f;

            while (true)
            {
                if (progressTime > LifeTime)
                {
                    break;
                }

                Color nowColor = Color.Lerp(
                    originalColor, targetColor, progressTime / LifeTime);
                text.color = nowColor;

                transform.Translate(Vector3.up * VerticalSpeed * Time.deltaTime);

                yield return null;

                progressTime += Time.deltaTime;
            }

            gameObject.SetActive(false);
        }
    }
}

