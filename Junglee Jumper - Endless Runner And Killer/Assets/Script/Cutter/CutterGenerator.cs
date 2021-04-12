using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler cutterPooler;
    [SerializeField] ObjectPooler verticalCutterPooler;
    [SerializeField] GameManager gameManager;
    public bool isCutterGenerated;
    private GameObject cutter;
    private GameObject verticalCutter;
    private int numberOfCutter;

    private void Start()
    {
        GameManager.isGameStart = false;
    }
  
    public void SpwanCutter(Vector3 position,float groundWidth)
    {
        int random = Random.Range(1, 100);
        if ((random < 60) && (groundWidth > 9))
        {
            isCutterGenerated = true;
            numberOfCutter = (int)Random.Range(1, 2);
            for (int i = 0; i < numberOfCutter; i++)
            {
                int distanceBetweenCutter = (int)Random.Range(1, (groundWidth/2));
                cutter = cutterPooler.GetPooledGameObject();
                verticalCutter = verticalCutterPooler.GetPooledGameObject();
                if (i == 0)
                {
                    int randomCutter = Random.Range(0, 100);
                    if (randomCutter < 50)
                    {
                        cutter.transform.position = new Vector3(position.x, position.y + 1, 100);
                        cutter.SetActive(true);
                    }
                    else if (randomCutter > 50)
                    {
                        verticalCutter.transform.position = new Vector3(position.x, position.y + 2.5f, 100);
                        verticalCutter.SetActive(true);
                    }
                }
                else if (i == 1)
                {
                    int randomCutter = Random.Range(0, 100);
                    if (randomCutter < 50)
                    {
                        cutter.transform.position = new Vector3(position.x + distanceBetweenCutter, position.y + 1, 100);
                        cutter.SetActive(true);
                    }
                    else if (randomCutter > 50)
                    {
                        verticalCutter.transform.position = new Vector3(position.x + distanceBetweenCutter, position.y + 2.5f, 100);
                        verticalCutter.SetActive(true);
                    }
                }
            }
        }
        else
        {
            isCutterGenerated = false;
        }

    }
}
