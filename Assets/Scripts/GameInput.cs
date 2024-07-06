using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    /// <summary>
    /// Получить нормализованный вектор движения 
    /// </summary>
    /// <returns>Нормализованный Вектор в 2D пространстве</returns>
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();

        return inputVector;
    }
}
