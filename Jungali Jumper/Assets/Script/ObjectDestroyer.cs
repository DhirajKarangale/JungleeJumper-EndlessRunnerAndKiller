using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private GameObject objectEndPoint;
    private void Start()
    {
        objectEndPoint = GameObject.Find("ObjectEndPoint");
    }

    private void Update()
    {
        if (transform.position.x < objectEndPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
