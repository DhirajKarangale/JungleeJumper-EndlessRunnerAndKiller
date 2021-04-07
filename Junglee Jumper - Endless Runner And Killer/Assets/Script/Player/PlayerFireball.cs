using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    [SerializeField] Zombie zombie;
    private Animator camAnimator;
    [SerializeField] GameObject fireballExplosionEffect;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    public static bool twoFireballCollide, playerFireballHitObject;
    private void Start()
    {
        twoFireballCollide = false;
        camAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerFireballHitObject = true;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

       else if (collision.gameObject.tag == "Zombie")
       {
            playerFireballHitObject = true;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            zombie.TakeDamage(damage);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
       }

       else if(collision.gameObject.tag == "ZombieFireball")
        {
            playerFireballHitObject = false;
            twoFireballCollide = true;
            Destroy(gameObject);
            camAnimator.SetBool("Shake", true);
            GameObject currentFireballExplosionEffect = Instantiate(fireballExplosionEffect, transform.position, transform.rotation);
            Destroy(currentFireballExplosionEffect, 4f);
        }
        else
        {
            playerFireballHitObject = false;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
        }
    }
}
