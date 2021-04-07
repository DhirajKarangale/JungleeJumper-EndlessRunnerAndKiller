using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    private Player player;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;
    [SerializeField] float damage;
    public static bool zombieFireballHitObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody.velocity = transform.right * (-speed);
    }

    private void Update()
    {
        if(PlayerFireball.twoFireballCollide)
        {
            Destroy(gameObject);
            PlayerFireball.twoFireballCollide = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if(!PlayerFireball.twoFireballCollide)
            {
                zombieFireballHitObject = true;
                Destroy(gameObject);
                GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                Destroy(currentImpactEffect, 1f);
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
           if(!PlayerFireball.twoFireballCollide)
           {
                zombieFireballHitObject = true;
                player.TakeDamege(damage);
                Destroy(gameObject);
                GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                Destroy(currentImpactEffect, 1f);
           }
        }
        else
        {
            zombieFireballHitObject = false;
        }
    }
}
