using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] GameObject cutedPlayer;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] GameObject bloodSplash;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject playerDestroyEffect;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform attackPoint;
    public bool isEnemyFireballAllowed;

    [Header("Attributes")]
    public float speed,originalSpeed;
    [SerializeField] float jumpForce, jumpTime;
    private float jumpTimeCounter;
    public float runingSpeedAnim;
    public bool playerRuning;
    public bool isPlayerDead,isPlayerHitObstacles;
    private Vector3 playerStartPosition;
    [SerializeField] float health;
    private float currentHealth;

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
        isEnemyFireballAllowed = true;
        levelDistanceCount = levelDistance;
        runingSpeedAnim = 1f;
        playerStartPosition = transform.position;
        originalSpeed = speed;
        isDashAllowed = true;
        currentHealth = health;
        healthSlider.value = currentHealth / health;
    }

    private void Update()
    {
        healthSlider.value = currentHealth / health;
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

        if(Input.GetKeyDown(KeyCode.A))
        {
            GameObject currentFireball = Instantiate(fireBall, attackPoint.position, attackPoint.rotation);
        }
    }

    private void Dash()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(2.172187f, 1.5f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2299106f, -1.15f);
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
        GetComponent<BoxCollider2D>().size = new Vector2(2.172187f, 3.740528f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2299106f, -0.06270278f);
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
        if(isGrounded)
        {
            isSwipeDown = true;
            if (dashSound.isPlaying) dashSound.Stop();
            dashSound.Play();
        }
        else
        {
            isSwipeDown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cutter")
        {
            isPlayerHitObstacles = true;
            DestroyPlayer();
        }
        else
        {
            isPlayerHitObstacles = false;
        }
    }

    public void TakeDamege(float damage)
    {
        if (currentHealth <= 0)
        {
            GameObject currentPlayerDestroyEffect = Instantiate(playerDestroyEffect, transform.position, transform.rotation);
            Destroy(currentPlayerDestroyEffect, 3f);
            isEnemyFireballAllowed = false;
            DestroyPlayer();
        }
        else
        {
            currentHealth -= damage;
        }
    }

    private void GameOverSound()
    {
        deathSound.Play();
    }
            
    private void DestroyPlayer()
    {
        Instantiate(cutedPlayer, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        GameObject currentBloodEffect = Instantiate(playerBloodEffect, transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position + new Vector3(0, -1, -1), transform.rotation);
        Destroy(currentBloodEffect, 2f);
        this.gameObject.SetActive(false);
        Invoke("GameOverSound", 1f);
        Invoke("PlayerDead", 2f);
    }

    private void PlayerDead()
    {
        isPlayerDead = true;
    }
}
