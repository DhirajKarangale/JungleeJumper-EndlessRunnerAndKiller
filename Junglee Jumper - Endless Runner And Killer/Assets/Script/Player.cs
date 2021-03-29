using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask deathGround;
    [SerializeField] ScrowllingBackGround scrowlling;

    [Header("Attributes")]
    public float speed,originalSpeed;
    [SerializeField] float jumpForce, jumpTime;
    private float jumpTimeCounter;
    public float runingSpeedAnim;
    public bool playerRuning;
    public static bool isPlayerDead;
    private Vector3 playerStartPosition;

    [Header("Dash")]
    [SerializeField] GameObject dashEffect;
    [SerializeField] float dashTime;
    [SerializeField] AudioSource dashSound;
    public float speedAfterdash;
    private bool isPlayerDash,isDashAllowed,isSwipeDown;

    [Header("Audio")]
    [SerializeField] AudioSource jumpSound;
    public AudioSource deathSound;

    [Header("Level")]
    [SerializeField] float levelDistance;
    private float levelDistanceCount;
    [SerializeField] float speedMultiplier;
    
    private bool isGrounded, doubleJumpAllowed, isJumping, isButtonPressed, isDoubleJump;
    

    private void Start()
    {
        levelDistanceCount = levelDistance;
        runingSpeedAnim = 1f;
        playerStartPosition = transform.position;
        originalSpeed = speed;
        isDashAllowed = true;
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

        if (!isButtonPressed && !isPlayerDead)
        {
            Jump();
        }

        if (transform.position.x > levelDistanceCount)
        {
            levelDistanceCount += levelDistance;
            speed = speed * speedMultiplier;
            runingSpeedAnim += (speedMultiplier/100);
            animator.SetFloat("RuningSpeed", runingSpeedAnim);
            levelDistance = levelDistance * speedMultiplier;
            scrowlling.backGroundSpeed += (speedMultiplier / 120);
            originalSpeed = speed;
        }

        if (isSwipeDown && isGrounded && isDashAllowed)
        {
            isDashAllowed = false;
            isPlayerDash = true;

        }

        if (isPlayerDash)
        {
            Dash();
        }

        if (isGrounded && !isPlayerDash)
        {
            animator.SetBool("PlayerDash", false);
            animator.SetBool("PlayerRuning", true);
        }
    }

    private void Dash()
    {
       
        GameObject currentDashEffect = Instantiate(dashEffect, transform.position + new Vector3(-0.5f, -1f, 0), Quaternion.identity);
        Destroy(currentDashEffect, 1f);
        isSwipeDown = false;
        speed = (originalSpeed * 1.5f);
        speedAfterdash = speed;
        animator.SetBool("PlayerDash", true);
        animator.SetBool("PlayerRuning", false);
        Invoke("DesableDash", dashTime);
    }

    private void DesableDash()
    {
        isSwipeDown = false;
        speed = (speedAfterdash / 1.5f);
        animator.SetBool("PlayerDash", false);
        animator.SetBool("PlayerRuning", true);
        isPlayerDash = false;
        isDashAllowed = true;
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
        animator.SetBool("PlayerRuning", false);
        animator.SetBool("isDoubleJump", isDoubleJump);
    }

    public void SwipeUp()
    {
        isSwipeDown = true;
         if (dashSound.isPlaying) dashSound.Stop();
        dashSound.Play();
    }
}
