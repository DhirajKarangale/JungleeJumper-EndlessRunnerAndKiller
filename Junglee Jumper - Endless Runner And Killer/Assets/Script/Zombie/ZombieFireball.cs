using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;
    public static bool zombieFireballHitObject,zombieFireballHitPlayer;

    private void Start()
    {
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
             zombieFireballHitObject = true;
                Destroy(gameObject);
                GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                Destroy(currentImpactEffect, 1f);
                zombieFireballHitPlayer = false;
        }
        else if(collision.gameObject.tag == "Player")
        {
           if(!PlayerFireball.twoFireballCollide)
           {
                zombieFireballHitObject = true;
                zombieFireballHitPlayer = true;
                Destroy(gameObject);
                GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                Destroy(currentImpactEffect, 1f);
           }
        }
        else if(collision.gameObject.tag == "Cutter")
        {
            zombieFireballHitObject = true;
                Destroy(gameObject);
                GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                Destroy(currentImpactEffect, 1f);
                zombieFireballHitPlayer = false;
        }
        else
        {
            zombieFireballHitObject = false;
            zombieFireballHitPlayer = false;
        }
    }
}
