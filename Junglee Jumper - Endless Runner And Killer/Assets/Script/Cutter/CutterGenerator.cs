using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] ObjectPooler cutterPooler;
    [SerializeField] ObjectPooler verticalCutterPooler;
    [SerializeField] GameManager gameManager;
    public int generator = 75;
    public bool isCutterGenerated;
    private GameObject cutter;
    private GameObject verticalCutter;

    private void Start()
    {
        GameManager.isGameStart = false;
    }
  
    public void SpwanCutter(Vector3 position,float groundWidth)
    {
        int random = Random.Range(1, 100);
        if ((random > generator) && (groundWidth > 9))
        {
            isCutterGenerated = true;
           int randomCutter = Random.Range(0, 100);
                    if (randomCutter < 50)
                    {
                        cutter = cutterPooler.GetPooledGameObject();
                        cutter.transform.position = new Vector3(position.x, position.y + 1, 100);
                        cutter.SetActive(true);
                    }
                    else if (randomCutter > 50)
                    {
                        verticalCutter = verticalCutterPooler.GetPooledGameObject();
                        verticalCutter.transform.position = new Vector3(position.x, position.y + 2.5f, 100);
                        verticalCutter.SetActive(true);
                    }
        }
        else
        {
            isCutterGenerated = false;
        }

    }
}
