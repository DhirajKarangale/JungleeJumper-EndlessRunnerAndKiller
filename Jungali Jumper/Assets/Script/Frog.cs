using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject fireBall;
    [SerializeField] float timeBetweenAttack;
    private float currentTimeBetweenAttack;

    private void Start()
    {
        currentTimeBetweenAttack = timeBetweenAttack;
    }

    private void Update()
    {
        if (!Player.isPlayerDead)
        {
            if (currentTimeBetweenAttack <= 0)
            {
                GameObject currentFireball = Instantiate(fireBall, attackPoint.position, attackPoint.rotation);
                Destroy(currentFireball, 3f);
                currentTimeBetweenAttack = timeBetweenAttack;
            }
            else
            {
                currentTimeBetweenAttack -= Time.deltaTime;
            }
        }
    }
}
