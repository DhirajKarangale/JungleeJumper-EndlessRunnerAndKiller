using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;

    public static bool zombieFireballHitPlayer;

    private void Start()
    {
        rigidBody.velocity = transform.right * (-speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation), 1f);
            zombieFireballHitPlayer = false;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {
            zombieFireballHitPlayer = true;
            Destroy(Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation), 1f);
            Destroy(gameObject);
        }
        else if((collision.gameObject.tag == "Cutter") || (collision.gameObject.tag == "VerC"))
        {
            Destroy(Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation), 1f);
            zombieFireballHitPlayer = false;
            Destroy(gameObject);
        }
        else
        {
            zombieFireballHitPlayer = false;
        }
    }
}
