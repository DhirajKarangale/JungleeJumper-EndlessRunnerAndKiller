using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask deathGround;
    [SerializeField] ScrowllingBackGround scrowlling;
    public float speed;
    public float originalSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] AudioSource jumpSound;
    public AudioSource deathSound;
    [SerializeField] float levelDistance;
    private float levelDistanceCount;
    [SerializeField] float speedMultiplier;
    private float jumpTimeCounter;
    private bool isGrounded, doubleJumpAllowed, isJumping, isButtonPressed, isDoubleJump;
    public bool isPlayerDead,playerRuning;
    private Vector3 playerStartPosition;
    public float runingSpeedAnim;

    private void Start()
    {
        originalSpeed = speed;
        levelDistanceCount = levelDistance;
        runingSpeedAnim = 1f;
        playerStartPosition = transform.position;
    }

    private void Update()
    {
        if (playerStartPosition == transform.position)
        {
            playerRuning = false;
        }
        else
        {
            playerRuning = true;
        }
        playerStartPosition = transform.position;

        isPlayerDead = Physics2D.IsTouchingLayers(playerCollider, deathGround);
        if (isPlayerDead) deathSound.Play();

        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        isGrounded = Physics2D.IsTouchingLayers(playerCollider, ground);
        if (isGrounded)
        {
            isDoubleJump = false;
        }

        if ((Input.touchCount > 0) && EventSystem.current != null)
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

        if (transform.position.x > levelDistanceCount)
        {
            levelDistanceCount += levelDistance;
            speed = speed * speedMultiplier;
            runingSpeedAnim += (speedMultiplier/20);
            animator.SetFloat("RuningSpeed", runingSpeedAnim);
            levelDistance = levelDistance * speedMultiplier;
            scrowlling.backGroundSpeed += (speedMultiplier / 27);
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
                if (jumpSound.isPlaying) jumpSound.Stop();
                jumpSound.Play();
            }
            else if (doubleJumpAllowed && ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
            {
                isDoubleJump = true;
                doubleJumpAllowed = false;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                if (jumpSound.isPlaying) jumpSound.Stop();
                jumpSound.Play();
            }
        }

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDoubleJump", isDoubleJump);
    }
}
