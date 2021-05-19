using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Refrences")]
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private Animator animator;
    [SerializeField] ZombieGenerator zombieGenerator;
    [SerializeField] CutterGenerator cutterGenerator;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask deathGround;
    [SerializeField] ScrowllingBackGround scrowlling;
    [SerializeField] GameObject cutedPlayer;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] GameObject bloodSplash;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject healthSliderGameObject;
    [SerializeField] GameObject dashButtonObject;
    [SerializeField] GameObject fireballButton;
    [SerializeField] GameObject playerDestroyEffect;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject fireBall2;
    [SerializeField] GameObject playerDeadBody;
    [SerializeField] GameObject zombieFireball;
    [SerializeField] Transform attackPoint;
    public bool isEnemyFireballAllowed;

    [Header("Attributes")]
    public float speed;
    [SerializeField] float jumpForce, jumpTime;
    private float jumpTimeCounter;
    public float runingSpeedAnim;
    public bool playerRuning;
    public bool isPlayerHitObstacles;
    public static bool isPlayerDead;
    private Vector3 playerStartPosition;
    public float health;
    public float currentHealth;
    [SerializeField] float zombieFireballDamage;

    [Header("Dash")]
    [SerializeField] Button dashButton;
    [SerializeField] Sprite dashImage;
    [SerializeField] Sprite crounchImage;
    [SerializeField] GameObject dashEffect;
    [SerializeField] float dashTime;
    [SerializeField] AudioSource dashSound;
    private bool isPlayerDash,isDashAllowed,isSwipeDown;

    [Header("Audio")]
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource fireBallCollisionSound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource hurtSound;
    public AudioSource runingSound;
    [SerializeField] AudioSource slideDownSound;
    public AudioSource gameOverSound;

    [Header("Level")]
    [SerializeField] float levelDistance;
    private float levelDistanceCount;
    [SerializeField] float speedMultiplier;
    
    private bool isGrounded, doubleJumpAllowed, isJumping, isButtonPressed, isDoubleJump;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        zombieFireball.SetActive(true);
        dashButtonObject.SetActive(true);
        fireballButton.SetActive(true);
        healthSliderGameObject.SetActive(true);
        isEnemyFireballAllowed = true;
        levelDistanceCount = levelDistance;
        runingSpeedAnim = 1f;
        playerStartPosition = transform.position;
        isDashAllowed = true;
        currentHealth = health;
        healthSlider.value = currentHealth / health;
    }

    private void Update()
    {
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

        isPlayerDead = Physics2D.IsTouchingLayers(playerCollider, deathGround);
        if (isPlayerDead) gameOverSound.Play();

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
        }
        
       
        if (!isGrounded && !isPlayerDash)
        {
            runingSound.Play();
        }


        if (PlayerFireball.twoFireballCollide)
        {
            if (fireBallCollisionSound.isPlaying) fireBallCollisionSound.Stop();
            fireBallCollisionSound.Play();
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
        Destroy(Instantiate(dashEffect, transform.position + new Vector3(-0.5f, -1f, 0), Quaternion.identity), 0.085f);
        isSwipeDown = false;
        animator.SetBool("PlayerDash", true);
        animator.SetBool("PlayerRuning", false);
        Invoke("DesableDash", dashTime);
    }

    private void DesableDash()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(2.172187f, 3.740528f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2299106f, -0.06270278f);
        isSwipeDown = false;
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
            DestroyPlayer();
        }
        else if(collision.gameObject.tag == "Zombie")
        {
            runingSound.Stop();
            scrowlling.backGroundSpeed = 0f;
            isPlayerHitObstacles = true;
            isEnemyFireballAllowed = false;
            zombieFireball.SetActive(false);
            dashButtonObject.SetActive(false);
            fireballButton.SetActive(false);
            Invoke("PlayerHitZombie", 0.3f);
        }
        else
        {
            isPlayerHitObstacles = false;
        }
    }

    public void TakeDamege(float damage)
    {
        if (hurtSound.isPlaying) hurtSound.Stop();
        hurtSound.Play();
        if (currentHealth <= 0)
        {
            Destroy(Instantiate(playerDestroyEffect, transform.position, transform.rotation), 3f);
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
        gameOverSound.Play();
    }
            
    private void DestroyPlayer()
    {
        runingSound.Stop();
        isPlayerHitObstacles = true;
        scrowlling.backGroundSpeed = 0f;
        deathSound.Play();
        currentHealth = 0;
        Instantiate(cutedPlayer, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Instantiate(bloodSplash, transform.position + new Vector3(0, -1, -1), transform.rotation);
        Destroy(Instantiate(playerBloodEffect, transform.position, Quaternion.identity), 2f);
        healthSliderGameObject.SetActive(false);
        dashButtonObject.SetActive(false);
        fireballButton.SetActive(false);
        this.gameObject.SetActive(false);
        zombieFireball.SetActive(false);
        Invoke("GameOverSound", 1f);
        Invoke("PlayerDead", 2f);
    }

    private void PlayerDead()
    {
        isPlayerDead = true;
    }

    private void PlayerHitZombie()
    {
        Destroy(Instantiate(playerDestroyEffect, transform.position, transform.rotation), 5f);
        
        speed = 0;
        deathSound.Play();
        gameObject.SetActive(false);
        Instantiate(playerDeadBody, transform.position, Quaternion.identity);
        currentHealth = 0;
        Invoke("GameOverSound", 1f);
        Invoke("PlayerDead", 2f);
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
}
