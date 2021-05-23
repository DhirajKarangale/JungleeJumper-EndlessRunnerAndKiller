using UnityEngine;

public class HealthPackGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler healthPackPooler;

    public void SpwanHealthPack(Vector3 position)
    {
        int random = Random.Range(1,100);
        if(random > 70) 
        {
        GameObject healthPack = healthPackPooler.GetPooledGameObject();
        healthPack.transform.position = new Vector3(position.x,position.y+2,0);
        healthPack.SetActive(true);
        }
    }
}
