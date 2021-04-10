using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler zombiePooler;
    [SerializeField] Player player;
    private float playerInitialPosition;

    private void Start()
    {
        playerInitialPosition = player.transform.position.x;
    }

    private void Update()
    {
        playerInitialPosition = player.transform.position.x;
    }

    public void SpwanZombie(Vector3 position)
    {
        if(playerInitialPosition > 150)
        {
           int platfrom = Random.Range(0, 100);
           if (platfrom < 50) return;
           GameObject zombie = zombiePooler.GetPooledGameObject();
           zombie.transform.position = new Vector3(position.x, position.y + 2, 0);
           zombie.SetActive(true);
        }
    }
}
