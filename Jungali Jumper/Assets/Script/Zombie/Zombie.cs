using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject fireBall;
    [SerializeField] float timeBetweenAttack;
    private float currentTimeBetweenAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();

        currentTimeBetweenAttack = timeBetweenAttack;
    }

    private void Update()
    {
        if (!Player.isPlayerDead)
        {
            if(currentTimeBetweenAttack <= 0)
            {

                animator.SetBool("Attack", true);
                animator.SetBool("Idel", false);
                animator.SetBool("Dye", false);
                Destroy(Instantiate(fireBall, attackPoint.position, attackPoint.rotation), 3f);
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
        animator.SetBool("Idel", true);
        animator.SetBool("Dye", false);
    }

}
