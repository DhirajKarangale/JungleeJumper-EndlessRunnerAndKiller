using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] AudioSource cutterSound;
    GameObject cutter;

    public void SpwanCutter(Vector3 position,float groundWidth)
    {
        int random = Random.Range(1, 100);
         if (random < 80) return;

        int numberOfCutter = (int) Random.Range(1, 2);
        for(int i = 0;i<numberOfCutter;i++)
        {
            int distanceBetweenCutter = (int)Random.Range(1, groundWidth);
            cutter = objectPooler.GetPooledGameObject();
            cutter.transform.position = new Vector3(position.x - (groundWidth/2) + distanceBetweenCutter + 3, position.y + 1, 10);
            cutter.SetActive(true);
        }

        float distanceBetweenPlayerAndCutter = player.transform.position.x - cutter.transform.position.x;
        if((distanceBetweenPlayerAndCutter < 3) && !cutterSound.isPlaying)
        {
            cutterSound.Play();
        }
        else
        {
            cutterSound.Stop();
        }
    }
}
