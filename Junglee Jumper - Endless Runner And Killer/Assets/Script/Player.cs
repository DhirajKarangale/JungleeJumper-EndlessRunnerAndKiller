using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] float speed;

    private void Update()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }
}
