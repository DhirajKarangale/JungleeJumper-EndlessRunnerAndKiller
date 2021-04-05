using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Zombie zombie;
    private Animator camAnimator;
    [SerializeField] GameObject fireballExplosionEffect;
    private AudioSource fireballExplosionSound;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    public static bool twoFireballCollide;
    private void Start()
    {
        fireballExplosionSound = GetComponent<AudioSource>();
        camAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>();
        rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

        if (collision.gameObject.tag == "Zombie")
        {
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            zombie.TakeDamage(damage);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

        if(collision.gameObject.tag == "ZombieFireball")
        {
            twoFireballCollide = true;
            Destroy(gameObject);
            fireballExplosionSound.Play();
            camAnimator.SetBool("Shake", true);
            GameObject currentFireballExplosionEffect = Instantiate(fireballExplosionEffect, transform.position, transform.rotation);
        }
        else
        {
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
        }
    }
}
