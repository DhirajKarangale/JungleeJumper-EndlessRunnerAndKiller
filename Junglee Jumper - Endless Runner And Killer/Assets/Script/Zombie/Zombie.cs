using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CircleCollider2D headColider;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] Player player;
    [SerializeField] GameObject zombieBloodSplash;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject enemieDestriyEffect;
    [SerializeField] GameObject sliderObject;
    [SerializeField] Slider enemyHealthSlider;
    [SerializeField] float health;
    private float currentHealth;
    [SerializeField] float attackRange;
    [SerializeField] float timeBetweenAttack;
    private float currentTimeBetweenAttack;
    private bool isZombieDead;

    private void Start()
    {
        sliderObject.SetActive(true);
        currentHealth = health;
        currentTimeBetweenAttack = timeBetweenAttack;
         enemyHealthSlider.value = currentHealth/health;
    }

    private void Update()
    {
        enemyHealthSlider.value = currentHealth/health;
        if ((player.transform.position.x < attackRange) && player.isEnemyFireballAllowed && !isZombieDead)
        {
            if(currentTimeBetweenAttack <= 0)
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Idel", false);
                animator.SetBool("Dye", false);
                GameObject currentFireball = Instantiate(fireBall, attackPoint.position, attackPoint.rotation);
                Destroy(currentFireball, 3f);
                currentTimeBetweenAttack = timeBetweenAttack;
                Invoke("SetAttackAnimToFalse", 0.5f);
            }
            else
            {
                currentTimeBetweenAttack -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            DestroyEnemie();
        }
        else
        {
            currentHealth -= damage;
        }
    }

    private void SetAttackAnimToFalse()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Idel", true);
        animator.SetBool("Dye", false);
    }

    private void DestroyEnemie()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Idel", false);
        animator.SetBool("Dye", true);
        isZombieDead = true;
        GameObject currentEnemieDestroyEffect = Instantiate(enemieDestriyEffect, transform.position, transform.rotation);
        Destroy(currentEnemieDestroyEffect,3f);
        GameObject currentZombieBloodSplash = Instantiate(zombieBloodSplash, transform.position + new Vector3(0, -2, -1), transform.rotation);
        Destroy(currentZombieBloodSplash, 5f);
        boxCollider.size = new Vector2(3.667627f, 1.569048f);
        boxCollider.offset = new Vector2(1.594227f, -2.260478f);
        headColider.radius = 1.934007f;
        headColider.offset = new Vector2(1.246684f, -0.34857f);
        sliderObject.SetActive(false);
    }
}
