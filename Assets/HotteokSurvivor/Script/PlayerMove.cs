using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [field: SerializeField] private float MoveSpeed { get; set; } = 1.0f;
    private int FacingDirection { get; set; } = 1;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = SimpleInput.GetMoveAxisRaw();
        float absMovement = Mathf.Abs(inputVector.x);
        Vector3 localscale = transform.localScale;

        if (absMovement > 0)
        {
            float currentScaleX = Mathf.Abs(localscale.x);
            FacingDirection = (inputVector.x < 0) ? -1 : 1;
            transform.localScale = new Vector3
                (currentScaleX * FacingDirection, localscale.y, localscale.z);
        }

        if (inputVector != Vector2.zero)
        {
            Vector3 moveVec = inputVector;

            transform.Translate(moveVec.normalized * MoveSpeed * Time.deltaTime);
        }
    }
}
