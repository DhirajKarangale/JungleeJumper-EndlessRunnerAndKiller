using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Player player;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform attackPoint;
    [SerializeField] float shootForce;
    [SerializeField] float impactForce;
    [SerializeField] float attackRange;
    [SerializeField] float timeBetweenShots;
    private float cuttertTimeBetweenShots;
    private float distanceBetweenPlayerAndZombie;

    private void Start()
    {
        cuttertTimeBetweenShots = timeBetweenShots;
    }

    private void Update()
    {
        distanceBetweenPlayerAndZombie = Vector3.Distance(player.transform.position, transform.position);

        Vector3 target = player.transform.position - transform.position;

        if(distanceBetweenPlayerAndZombie <= attackRange)
        {
            if (cuttertTimeBetweenShots <= 0)
            {
                GameObject currentFireball = Instantiate(fireBall, attackPoint.position, Quaternion.identity);
                transform.forward = target.normalized;
                currentFireball.GetComponent<Rigidbody2D>().AddForce(target.normalized * shootForce, ForceMode2D.Impulse);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, target);
                if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * impactForce);
                cuttertTimeBetweenShots = timeBetweenShots;
                Destroy(currentFireball, 3f);
            }
            else cuttertTimeBetweenShots -= Time.deltaTime;
        }
    }
}
