using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ObjectPooler cutterPooler;
    [SerializeField] ObjectPooler verticalCutterPooler;
    [SerializeField] AudioSource cutterSound;
    [SerializeField] GameManager gameManager;
    public bool isCutterGenerated;
    private GameObject cutter;
    private GameObject verticalCutter;
    private int numberOfCutter;
    private float playerInitialPosition;

    private void Start()
    {
        playerInitialPosition = player.transform.position.x;
        gameManager.isGameStart = false;
    }

    private void Update()
    {
        playerInitialPosition = player.transform.position.x;
    }

    public void SpwanCutter(Vector3 position,float groundWidth)
    {
        int random = Random.Range(1, 100);
        if ((playerInitialPosition < 100) && (random < 85))
        {
            isCutterGenerated = false;
            return;
        }
        else if ((playerInitialPosition < 200) && (random < 75))
        {
            isCutterGenerated = false;
            return;
        }
        else if ((playerInitialPosition < 400) && (random < 70))
        {
            isCutterGenerated = false;
            return;
        }
        else if ((playerInitialPosition < 550) && (random < 60))
        {
            isCutterGenerated = false;
            return;
        }
        else if ((playerInitialPosition < 750) && (random < 50))
        {
            isCutterGenerated = false;
            return;
        } 
         isCutterGenerated = true;
            if (groundWidth > 7)
            {
                numberOfCutter = (int)Random.Range(1, 2);
            }
            else
            {
                numberOfCutter = 1;
            }
            for (int i = 0; i < numberOfCutter; i++)
            {
                int distanceBetweenCutter = (int)Random.Range(1, (groundWidth/2));
                cutter = cutterPooler.GetPooledGameObject();
                verticalCutter = verticalCutterPooler.GetPooledGameObject();
                if (i == 0)
                {
                    int randomCutter = Random.Range(0, 100);
                    if (randomCutter < 50 || (groundWidth > 7))
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

            float distanceBetweenPlayerAndCutter = cutter.transform.position.x - transform.position.x;
            if ((distanceBetweenPlayerAndCutter < 30) && !player.isPlayerDead && gameManager.isGameStart)
            {
                cutterSound.Play();
            }
            else
            {
                cutterSound.Stop();
            }
    }
}
