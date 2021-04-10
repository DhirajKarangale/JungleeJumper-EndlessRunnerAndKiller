using UnityEngine;

public class GroundPoolers : MonoBehaviour
{
    [SerializeField] Transform groundPoint;
    [SerializeField] Transform minHeightPoint;
    [SerializeField] Transform maxHeightPoint;
    [SerializeField] ObjectPooler[] groundPoolers;
    [SerializeField] CoinGenerator coinGenerator;
    [SerializeField] CutterGenerator cutterGenerator;
    [SerializeField] ZombieGenerator zombieGenerator;
    [SerializeField] HealthPackGenerator healthPackGenerator;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Player player;
    private float minY, maxY;
    [SerializeField] float minGap, maxGap;
    private float[] groundWidths;

    private void Start()
    {
        minY = minHeightPoint.position.y;
        maxY = maxHeightPoint.position.y;
        groundWidths = new float[groundPoolers.Length];

        for (int i=0;i<groundPoolers.Length;i++)
        {
            groundWidths[i] = groundPoolers[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    private void Update()
    {
        if((transform.position.x < groundPoint.position.x) && !scoreManager.isPause)
        {
            int random = Random.Range(0, groundPoolers.Length);
            float distance = groundWidths[random] / 2;
            float gap = Random.Range(minGap, maxGap);
            float height = Random.Range(minY, maxY);
            transform.position = new Vector3(transform.position.x + distance + gap, height , transform.position.z);

            GameObject ground = groundPoolers[random].GetPooledGameObject();
            ground.transform.position = transform.position;
            ground.SetActive(true);


            coinGenerator.SpwanCoin(transform.position, groundWidths[random]);

            cutterGenerator.SpwanCutter(transform.position, groundWidths[random]);

            if(!cutterGenerator.isCutterGenerated) zombieGenerator.SpwanZombie(transform.position);

            if((player.currentHealth < player.health) && !player.isPlayerDead) healthPackGenerator.SpwanHealthPack(transform.position);

            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }
    }
}
