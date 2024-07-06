using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameInput gameInput;

    private Animator animator;

    private Vector2 inputVector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputVector = gameInput.GetMovementVectorNormalized();

        AnimationHandle();
        FlipSpriteHandle();
    }

    private void AnimationHandle()
    {
        if (inputVector.x != 0 || inputVector.y != 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }

    private void FlipSpriteHandle()
    {
        if (inputVector.x > 0)
            playerSprite.flipX = false;
        else if (inputVector.x < 0)
            playerSprite.flipX = true;
    }
}
