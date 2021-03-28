using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ground;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    private float jumpTimeCounter;
    private bool isGrounded,doubleJumpAllowed,isJumping,isButtonPressed,isDoubleJump;

    private void Update()
    {

        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        isGrounded = Physics2D.IsTouchingLayers(playerCollider, ground);
        if(isGrounded)
        {
            isDoubleJump = false;
        }

        if((Input.touchCount>0) && EventSystem.current != null)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                isButtonPressed = true;
            }
            else
            {
                isButtonPressed = false;
            }
        }

        if (!isButtonPressed)
        {
            Jump();
        }

    }

    

    private void Jump()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Stationary) && (isJumping))
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
            if (isGrounded && ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
            {
                jumpTimeCounter = jumpTime;
                isJumping = true;
                doubleJumpAllowed = true;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            }
            else if (doubleJumpAllowed && ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
            {
                isDoubleJump = true;
                doubleJumpAllowed = false;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            }
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDoubleJump", isDoubleJump);
    }

    public void Bu()
    {
        Debug.Log("Button Press Sucess");
    }

}
