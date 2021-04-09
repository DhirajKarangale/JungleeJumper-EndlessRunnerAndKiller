using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Animator camAnimator;
    private AudioSource firballReleseSound;
    [SerializeField] GameObject fireballExplosionEffect;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    public static bool twoFireballCollide, playerFireballHitObject,playerFireballCollideZombie,playerFireballCollideWithCutter;
    private void Start()
    {
        firballReleseSound = GetComponent<AudioSource>(); 
        twoFireballCollide = false;
        camAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        rigidBody.velocity = transform.right * speed;
        firballReleseSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            firballReleseSound.Stop();
            playerFireballCollideWithCutter = false;
            playerFireballCollideZombie = false;
            playerFireballHitObject = true;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

       else if (collision.gameObject.tag == "Zombie")
       {
            firballReleseSound.Stop();
            playerFireballCollideWithCutter = false;
            playerFireballCollideZombie = true;
            playerFireballHitObject = true;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
       }

       else if(collision.gameObject.tag == "ZombieFireball")
        {
            firballReleseSound.Stop();
            playerFireballCollideWithCutter = false;
            playerFireballCollideZombie = false;
            playerFireballHitObject = false;
            twoFireballCollide = true;
            Destroy(gameObject);
            camAnimator.SetBool("Shake", true);
            GameObject currentFireballExplosionEffect = Instantiate(fireballExplosionEffect, transform.position, transform.rotation);
            Destroy(currentFireballExplosionEffect, 4f);
        }
        else if(collision.gameObject.tag == "Cutter")
        {
            playerFireballCollideWithCutter = true;
            firballReleseSound.Stop();
            playerFireballCollideZombie = false;
            playerFireballHitObject = true;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        else
        {
            playerFireballCollideWithCutter = false;
            playerFireballCollideZombie = false;
            playerFireballHitObject = false;
            twoFireballCollide = false;
            camAnimator.SetBool("Shake", false);
        }
    }
}
