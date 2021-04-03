using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] Player player;
    [SerializeField] GameObject fireBall;
    [SerializeField] float attackRange;
    [SerializeField] float timeBetweenAttack;
    private float currentTimeBetweenAttack;

    private void Start()
    {
        currentTimeBetweenAttack = timeBetweenAttack;
    }

    private void Update()
    {
        if(player.transform.position.x < attackRange)
        {
            if(currentTimeBetweenAttack <= 0)
            {
                animator.SetBool("Attack", true);
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

    private void SetAttackAnimToFalse()
    {
        animator.SetBool("Attack", false);
    }
}
