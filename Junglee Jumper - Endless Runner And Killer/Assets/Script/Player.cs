using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float speed;

    private void Update()
    {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }
}
