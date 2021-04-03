using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    private Player player;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;
    [SerializeField] int damage;

    private void Start()
    {
        rigidbody.velocity = transform.right * speed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 0.1f);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1,0,0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamege(damage);
            Destroy(gameObject, 0.1f);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
    }
}
