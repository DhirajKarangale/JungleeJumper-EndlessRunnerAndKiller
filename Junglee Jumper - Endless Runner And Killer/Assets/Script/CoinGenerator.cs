using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler coinPooler;

    public void SpwanCoin(Vector3 position, float groundWidth)
    {
        int coinPlatform = Random.Range(1, 100);
        if (coinPlatform < 55) return;

        int numberOfCoin = (int)Random.Range(2f, groundWidth/1.3f);

        float heightOfCoin = Random.Range(2, 5.5f);
        for(int i=0;i<numberOfCoin;i++)
        {
            GameObject coin = coinPooler.GetPooledGameObject();
            coin.transform.position = new Vector3(position.x - ((groundWidth/2)-2)+i, position.y+2 + heightOfCoin, 0);
            coin.SetActive(true);
        }
    }
}
