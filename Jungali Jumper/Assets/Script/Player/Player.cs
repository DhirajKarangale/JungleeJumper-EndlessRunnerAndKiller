using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private Animator animator;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject cutedPlayer;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] GameObject bloodSplash;
    [SerializeField] GameObject playerDestroyEffect;
    [SerializeField] GameObject playerDeadBody;
    [SerializeField] Transform attackPoint;


    [Header("Attributes")]
    public float speed;
    [SerializeField] float jumpForce, jumpTime;
    private float jumpTimeCounter;
    public float runingSpeedAnim;
    public bool playerRuning;
    public static bool isPlayerFallDown, isGameOver ,isPlayerDead;
    private Vector3 playerStartPosition;
    public float health;
    public float currentHealth;
    [SerializeField] float zombieFireballDamage;

    [Header("Dash")]
    [SerializeField] Button dashButton;
    [SerializeField] Sprite dashImage;
    [SerializeField] Sprite crounchImage;
    [SerializeField] GameObject dashEffect;
    [SerializeField] GameObject dashEffect2;
    [SerializeField] float dashTime;
    [SerializeField] AudioSource dashSound;
    private bool isPlayerDash, isDashAllowed, isSwipeDown;

    [Header("Layer Mask")]
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask deathGround;
    
    [Header("Script Refrence")]
    [SerializeField] ZombieGenerator zombieGenerator;
    [SerializeField] CutterGenerator cutterGenerator;
    [SerializeField] ScrowllingBackGround scrowlling;

    [Header("Objects")]
    [SerializeField] GameObject dashButtonObject;
    [SerializeField] GameObject fireballButton;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject fireBall2;
    [SerializeField] GameObject zombieFireball;

    [Header("Audio")]
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] AudioSource slideDownSound;
    [SerializeField] AudioSource runingSound;

    [Header("Level")]
    [SerializeField] float levelDistance;
    private float levelDistanceCount;
    [SerializeField] float speedMultiplier;
    
    private bool isGrounded, doubleJumpAllowed, isJumping, isButtonPressed, isDoubleJump;
    
    private void Start()
    {
        isPlayerDead = false;
        isGameOver = false;
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
       
        zombieFireball.SetActive(true);
      
        dashButtonObject.SetActive(true);
        fireballButton.SetActive(true);
        healthSlider.gameObject.SetActive(true);
      
        isDashAllowed = true;
     
        levelDistanceCount = levelDistance;
        currentHealth = health;
        playerStartPosition = transform.position;
        healthSlider.value = currentHealth / health;
        runingSpeedAnim = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireBallButton();
        }

        healthSlider.value = currentHealth / health;
        if (playerStartPosition.x == transform.position.x)
        {
            playerRuning = false;
        }
        else
        {
            playerRuning = true;
        }
        playerStartPosition = transform.position;

        isPlayerFallDown = Physics2D.IsTouchingLayers(playerCollider, deathGround);
        if (isPlayerFallDown) Invoke("SetGameOverTrue", 0.2f);

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
            zombieGenerator.generator -= 3;
            cutterGenerator.generator -= 6;
            levelDistance = levelDistance * speedMultiplier;
            scrowlling.backGroundSpeed += (speedMultiplier / 120);
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
            if (!ScoreManager.isPause && GameManager.isGameStart && !isPlayerDead) runingSound.Play();
            else runingSound.Pause();
        }
             

        if(ZombieFireball.zombieFireballHitPlayer)
        {
            TakeDamege(zombieFireballDamage);
            ZombieFireball.zombieFireballHitPlayer = false;
        }

        if(!isGrounded) dashButton.image.sprite = crounchImage;
        else dashButton.image.sprite = dashImage;
    }

    private void Dash()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(2.172187f, 1.5f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2299106f, -1.15f);
        if(GameDataVariable.dataVariables[7] == 2)
        {
            Destroy(Instantiate(dashEffect2, transform.position + new Vector3(-0.5f, -1f, 0), Quaternion.identity), 0.099f);
        }
        else
        {
            Destroy(Instantiate(dashEffect, transform.position + new Vector3(-0.5f, -1f, 0), Quaternion.identity), 0.099f);
        }
        isSwipeDown = false;
        animator.SetBool("PlayerDash", true);
        animator.SetBool("PlayerRuning", false);
        runingSound.Pause();
        Invoke("DesableDash", dashTime);
    }

    private void DesableDash()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(2.172187f, 3.740528f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2299106f, -0.06270278f);
        isSwipeDown = false;
        animator.SetBool("PlayerDash", false);
        animator.SetBool("PlayerRuning", true);
        if (!ScoreManager.isPause && GameManager.isGameStart && !isPlayerDead) runingSound.Play();
        else runingSound.Pause();
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
       else   if (isGrounded && ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
       {
                isJumping = false;
                jumpTimeCounter = jumpTime;
                isJumping = true;
                doubleJumpAllowed = true;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                if (jumpSound.isPlaying) jumpSound.Stop();
                jumpSound.Play();
       }
        else if (doubleJumpAllowed && ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
        {
                isJumping = false;
                isDoubleJump = true;
                doubleJumpAllowed = false;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                if (jumpSound.isPlaying) jumpSound.Stop();
                jumpSound.Play();
        }
        

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("PlayerRuning", false);
        runingSound.Pause();
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
            slideDownSound.Play();
            rigidBody.AddForce(-transform.up * 40 , ForceMode2D.Impulse);
            isSwipeDown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Cutter") || (collision.gameObject.tag == "VerC"))
        {
            CutPlayer();
        }
        else if(collision.gameObject.tag == "Zombie")
        {

            PlayerHitZombie();
        }
    }

    public void TakeDamege(float damage)
    {
        if (hurtSound.isPlaying) hurtSound.Stop();
        hurtSound.Play();

        if (currentHealth <= 0)
        {
            CutPlayer();
        }
        else
        {
            currentHealth -= damage;
        }
    }
            
    private void CutPlayer()
    {
        runingSound.Stop();
        isPlayerDead = true;
        scrowlling.backGroundSpeed = 0f;
        currentHealth = 0;
        Instantiate(cutedPlayer, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Instantiate(bloodSplash, transform.position + new Vector3(0, -1, -1), transform.rotation);
        Destroy(Instantiate(playerBloodEffect, transform.position, Quaternion.identity), 2f);
        healthSlider.gameObject.SetActive(false);
        dashButtonObject.SetActive(false);
        fireballButton.SetActive(false);
        this.gameObject.SetActive(false);
        zombieFireball.SetActive(false);
        Invoke("SetGameOverTrue", 2f);
    }

    private void PlayerHitZombie()
    {
        isPlayerDead = true;
        runingSound.Stop();
        scrowlling.backGroundSpeed = 0f;
        speed = 0;
        currentHealth = 0;
        zombieFireball.SetActive(false);
        dashButtonObject.SetActive(false);
        fireballButton.SetActive(false);
        gameObject.SetActive(false);
        Destroy(Instantiate(playerDestroyEffect, transform.position, transform.rotation), 5f);
        Instantiate(playerDeadBody, transform.position, Quaternion.identity);
        Invoke("SetGameOverTrue", 2f);
    }

    public void FireBallButton()
    {
     if(GameManager.isGameStart)
     {
            if(GameDataVariable.dataVariables[3] == 2)
            {
                GameObject currentFireball = Instantiate(fireBall2, attackPoint.position , Quaternion.Euler(0,0,180));
                Destroy(currentFireball, 1.6f);
                if (isPlayerDead) Destroy(currentFireball);
            }
            else
            {
                GameObject currentFireball = Instantiate(fireBall, attackPoint.position, attackPoint.rotation);
                Destroy(currentFireball, 1.3f);
                if (isPlayerDead) Destroy(currentFireball);
            }
     }
    }

    private void SetGameOverTrue()
    {
        speed = 0;
        isPlayerDead = true;
        isGameOver = true;
    }
}
