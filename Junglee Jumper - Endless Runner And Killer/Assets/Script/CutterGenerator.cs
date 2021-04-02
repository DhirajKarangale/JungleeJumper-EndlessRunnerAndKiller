using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ObjectPooler cutterPooler;
    [SerializeField] ObjectPooler verticalCutterPooler;
    [SerializeField] AudioSource cutterSound;
    private GameObject cutter;
    private GameObject verticalCutter;
    private int numberOfCutter;
    private float playerInitialPosition;

    private void Start()
    {
        playerInitialPosition = player.transform.position.x;
    }

    public void SpwanCutter(Vector3 position,float groundWidth)
    {
        int random = Random.Range(1, 100);
        if (playerInitialPosition < 100) if (random < 90) return;
        else if (playerInitialPosition < 200) if (random < 80) return;
        else if (playerInitialPosition < 400) if (random < 70) return;
        else if (playerInitialPosition < 550) if (random < 60) return;
        else if (playerInitialPosition < 750) if (random < 50) return;

        if (groundWidth > 7)
        {
            numberOfCutter = (int)Random.Range(1, 3);
        }
        else
        {
            numberOfCutter = 1;
        }
        for(int i = 0;i<numberOfCutter;i++)
        {
            int distanceBetweenCutter = (int)Random.Range(1, groundWidth);
            cutter = cutterPooler.GetPooledGameObject();
            verticalCutter = verticalCutterPooler.GetPooledGameObject();
            if(i == 0)
            {
                int randomCutter = Random.Range(0, 100);
                if(randomCutter<50 || (groundWidth > 7))
                {
                    cutter.transform.position = new Vector3(position.x, position.y + 1, 10);
                    cutter.SetActive(true);
                }
                else if((randomCutter > 50))
                {
                    verticalCutter.transform.position = new Vector3(position.x, position.y + 2.5f, 10);
                    verticalCutter.SetActive(true);
                }
            }
            else if(i == 2)
            {
                int randomCutter = Random.Range(0, 100);
                if (randomCutter < 50)
                {
                    cutter.transform.position = new Vector3(position.x + distanceBetweenCutter, position.y + 1, 10);
                    cutter.SetActive(true);
                }
                else
                {
                    verticalCutter.transform.position = new Vector3(position.x + distanceBetweenCutter, position.y + 2.5f, 10);
                    verticalCutter.SetActive(true);
                }
            }
            
        }

        float distanceBetweenPlayerAndCutter = cutter.transform.position.x - transform.position.x;
        if ((distanceBetweenPlayerAndCutter < 30) && !player.isPlayerDead)
        {
            cutterSound.Play();
        }
        else
        {
            cutterSound.Stop();
        }
    }
}
