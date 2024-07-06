using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameInput gameInput;

    private Rigidbody2D rb;
    private Vector2 inputVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHandle();
    }

    /// <summary>
    /// Обрабатываем движения
    /// </summary>
    private void MoveHandle()
    {
        inputVector = gameInput.GetMovementVectorNormalized();

        rb.velocity = inputVector * movementSpeed;
    }
}
