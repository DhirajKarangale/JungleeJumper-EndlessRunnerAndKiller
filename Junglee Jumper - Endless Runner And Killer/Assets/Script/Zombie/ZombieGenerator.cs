using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler zombiePooler;
    [SerializeField] ObjectPooler frogPooler;
    public int generator = 90;

    public void SpwanZombie(Vector3 position)
    {
        int platfrom = Random.Range(0, 100);
           if (platfrom > generator)
           {
            if(GameDataVariable.dataVariables[5] == 2)
            {
                int random = Random.Range(0, 100);
                if (random < 50)
                {
                    GameObject frog = frogPooler.GetPooledGameObject();
                    frog.transform.position = new Vector3(position.x, position.y + 2, 0);
                    frog.SetActive(true);
                }
                else
                {
                    GameObject zombie = zombiePooler.GetPooledGameObject();
                    zombie.transform.position = new Vector3(position.x, position.y + 2, 0);
                    zombie.SetActive(true);
                }
            }
            else
            {
                GameObject zombie = zombiePooler.GetPooledGameObject();
                zombie.transform.position = new Vector3(position.x, position.y + 2, 0);
                zombie.SetActive(true);
            }
           }
    }
}
