using UnityEngine;

public class Frog : MonoBehaviour
{
    private Player player;
    private ScoreManager scoreManager;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject zombieBloodSplash;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject enemieDestriyEffect;
    [SerializeField] float timeBetweenAttack;
    private float currentTimeBetweenAttack;
    public static bool isZombieDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentTimeBetweenAttack = timeBetweenAttack;
        isZombieDead = false;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (player.isEnemyFireballAllowed && !Player.isPlayerDead)
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

        if (PlayerFireball.playerFireballCollideZombie)
        {
            DestroyEnemie();
            scoreManager.score += 15;
        }
    }

    private void DestroyEnemie()
    {
        isZombieDead = true;
        GameObject currentEnemieDestroyEffect = Instantiate(enemieDestriyEffect, transform.position, transform.rotation);
        Destroy(currentEnemieDestroyEffect, 3f);
        GameObject currentZombieBloodSplash = Instantiate(zombieBloodSplash, transform.position + new Vector3(0, -2, -1), transform.rotation);
        Destroy(currentZombieBloodSplash, 5f);
        gameObject.SetActive(false);
        PlayerFireball.playerFireballCollideZombie = false;
    }
}
