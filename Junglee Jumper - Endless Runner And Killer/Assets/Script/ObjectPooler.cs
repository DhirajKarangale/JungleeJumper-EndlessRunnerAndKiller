using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    [SerializeField] int numberOfObject;
    private List<GameObject> gameObjects;

    private void Start()
    {
        gameObjects = new List<GameObject>();
        
        for(int i=0;i<numberOfObject;i++)
        {
            GameObject gameObject = Instantiate(pooledObject);
            gameObject.SetActive(false);
            gameObjects.Add(gameObject);
        }
    }

    public GameObject GetPooledGameObject()
    {
        foreach(GameObject gameObject in gameObjects)
        {
            if (!gameObject.activeInHierarchy)
                return gameObject;
        }

        GameObject gameObject1 = Instantiate(pooledObject);
        gameObject1.SetActive(false);
        gameObjects.Add(gameObject1);
        return gameObject1;
    }
}
