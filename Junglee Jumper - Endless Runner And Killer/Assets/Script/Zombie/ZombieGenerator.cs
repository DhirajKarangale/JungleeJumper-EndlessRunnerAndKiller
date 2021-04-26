using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler zombiePooler;
    public int generator = 90;

    public void SpwanZombie(Vector3 position)
    {
        int platfrom = Random.Range(0, 100);
           if (platfrom > generator)
          {
           GameObject zombie = zombiePooler.GetPooledGameObject();
           zombie.transform.position = new Vector3(position.x, position.y + 2, 0);
           zombie.SetActive(true);
          }
    }
}
