using UnityEngine;

public class GroundPoolers : MonoBehaviour
{
    [SerializeField] Transform groundPoint;
    [SerializeField] Transform minHeightPoint;
    [SerializeField] Transform maxHeightPoint;
    [SerializeField] ObjectPooler[] groundPoolers;
    private float minY, maxY;
    [SerializeField] float minGap, maxGap;
    private float[] groundWidths;

    private void Start()
    {
        minY = minHeightPoint.position.y;
        maxY = maxHeightPoint.position.y;
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
            float gap = Random.Range(minGap, maxGap);
            float height = Random.Range(minY, maxY);
            transform.position = new Vector3(transform.position.x + distance + gap, height , transform.position.z);

            GameObject ground = groundPoolers[random].GetPooledGameObject();
            ground.transform.position = transform.position;
            ground.SetActive(true);
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }
    }
}
