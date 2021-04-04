using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    private Player player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;
    [SerializeField] float damage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody.velocity = transform.right * (-speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1,0,0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        if(collision.gameObject.tag == "Player")
        {
            if (audioSource.isPlaying) audioSource.Stop();
            audioSource.Play(); 
            player.TakeDamege(damage);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
    }
}
