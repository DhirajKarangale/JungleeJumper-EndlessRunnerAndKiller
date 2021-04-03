using UnityEngine;

public class ZombieFireball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float speed;
    [SerializeField] float damage;

    private void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Player"))
        {
            Destroy(gameObject, 0.1f);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(-1,0,0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
    }
}
