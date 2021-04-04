using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Zombie zombie;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    private void Start()
    {
        zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>();
        rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

        if (collision.gameObject.tag == "Zombie")
        {
            zombie.TakeDamage(damage);
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
    }
}
