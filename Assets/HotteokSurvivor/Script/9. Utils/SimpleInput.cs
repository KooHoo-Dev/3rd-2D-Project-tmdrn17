using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Study.Utilities
{
    // PC 플랫폼에서 사용하는 키보드, 마우스 인풋들을 묶어 놓은 Util 클래스
    // 쉽고 직관적으로 사용할 수 있도록 묶는 작업

    // 이러한 디자인 패턴을 Wrapping 한다는 뜻으로
    // Wrapper 패턴 이라고 부른다.

    // SimpleInput 클래스는 어디서든 호출 가능하게
    // static(정적, 고정된, 랜드마크) 키워드를 사용해 선언한다.
    public static class SimpleInput
    {
        #region 키보드 입력
        public static bool GetKeyDown(Key key)
        {
            return Keyboard.current[key].wasPressedThisFrame;
        }

        public static bool GetKeyUp(Key key)
        {
            return Keyboard.current[key].wasReleasedThisFrame;
        }

        public static bool GetKey(Key key)
        {
            return Keyboard.current[key].isPressed;
        }

        public static bool AnyKeyDown()
        {
            return Keyboard.current.anyKey.wasPressedThisFrame;
        }

        #endregion

        #region 마우스 입력

        /// <summary>
        /// 마우스 버튼 종류를 정의하는 열거형
        /// </summary>
        public enum MouseButton
        {
            Left=0,
            Right=1,
            Middle=2
        }

        private static ButtonControl GetMouseControll(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return Mouse.current.leftButton;
                case MouseButton.Right:
                    return Mouse.current.rightButton;
                case MouseButton.Middle:
                    return Mouse.current.middleButton;
            }

            return null;
        }

        public static bool GetMouseButtonDown(MouseButton button)
        {
            return GetMouseControll(button).wasPressedThisFrame;
        }

        public static bool GetMouseButtonUp(MouseButton button)
        {
            return GetMouseControll(button).wasReleasedThisFrame;
        }

        public static bool GetMouseButton(MouseButton button)
        {
            return GetMouseControll(button).isPressed;
        }

        public static Vector2 GetMousePosition()
        {
            return Mouse.current.position.ReadValue();
        }

        /// <summary>
        /// 마우스의 한 프레임에 이동한 위치 변화량 반환
        /// </summary>
        /// <returns></returns>

        public static Vector2 GetMouseDelta()
        {
            return Mouse.current.delta.ReadValue();
        }

        public static float GetMouseScrollDeltaRaw()
        {
            return Mouse.current.scroll.ReadValue().y;
        }

        // 마우스 휠 스크롤을 -1, 0, 1 단위로 반환
        public static float GetMouseScrollDelta()
        {
            float raw=GetMouseScrollDeltaRaw();

            // Mathf.Approximately(a, b)
            // b랑 비교를 할 경우 근사치로 일정한 범위 내에 a가 있는지 비교
            // ex) 0.999999 = 1
            return Mathf.Approximately(raw, 0f) ? 0f : Mathf.Sign(raw);
        }

        #endregion

        #region 축 입력 (WASD / 방향키)

        /// <summary>
        ///  수평축 원시 입력값 반환 (-1, 0, 1)
        ///  A/D 키 또는 좌/우 화살표 키 대응 
        ///  (레거시 에서 Input.GetAxisRaw("Horizontal")과 동일)
        /// </summary>
        /// <returns></returns>

        public static float GetAxisHorizontalRaw()
        {
            float axis = 0.0f;

            if(Keyboard.current.dKey.isPressed|| Keyboard.current.rightArrowKey.isPressed)
            {
                axis += 1.0f;
            }

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                axis -= 1.0f;
            }

            return axis;
        }

        /// <summary>
        ///  수직축 원시 입력값 반환 (-1, 0, 1)
        ///  W/S 키 또는 위/아래 화살표 키 대응
        ///  (레거시 에서 Input.GetAxisRaw("Vertical")과 동일)
        /// </summary>
        /// <returns></returns>
        
        public static float GetAxisVerticalRaw()
        {
            float axis = 0.0f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            {
                axis += 1.0f;
            }

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            {
                axis -= 1.0f;
            }

            return axis;
        }

        /// <summary>
        /// 수평 / 수직 Raw 입력을 합쳐 정규화된 Vector2로 반환
        /// 대각선 입력시 좌표계에서 대각선이동은
        /// 피타고라스 정리를 따라서 루트2 만큼의 길이를 가지게 되므로
        /// 한방향 이동인 1 보다 커져서 속도가 조금 빨라지는 현상을
        /// .nomalized 함수로 보정하여 반환
        /// </summary>
        /// <returns></returns>

        public static Vector2 GetMoveAxisRaw()
        {
            Vector2 axis = new Vector2();
            axis.x = GetAxisHorizontalRaw();
            axis.y = GetAxisVerticalRaw();

            if (axis.sqrMagnitude > 1f)
            {
                return axis.normalized;
            }

            return axis;
        }

        #endregion
    }
}

