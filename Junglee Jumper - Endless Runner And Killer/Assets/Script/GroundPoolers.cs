using UnityEngine;

public class GroundPoolers : MonoBehaviour
{
    [SerializeField] Transform groundPoint;
    [SerializeField] ObjectPooler[] groundPoolers;
    private float[] groundWidths;

    private void Start()
    {
        groundWidths = new float[groundPoolers.Length];

        for(int i=0;i<groundPoolers.Length;i++)
        {
            groundWidths[i] = groundPoolers[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    private void Update()
    {
        if(transform.position.x<groundPoint.position.x)
        {
            int random = Random.Range(0, groundPoolers.Length);
            float distance = groundWidths[random] / 2;

            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);

            GameObject ground = groundPoolers[random].GetPooledGameObject();
            ground.transform.position = transform.position;
            ground.SetActive(true);
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }
    }
}
