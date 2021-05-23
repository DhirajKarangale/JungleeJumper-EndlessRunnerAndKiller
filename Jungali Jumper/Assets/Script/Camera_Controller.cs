using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private Player player;
    private Vector3 lastPosition;
    private float distance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        lastPosition = player.transform.position;
    }

    private void Update()
    {
        distance = player.transform.position.x - lastPosition.x;
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        lastPosition = player.transform.position;
    }
}
