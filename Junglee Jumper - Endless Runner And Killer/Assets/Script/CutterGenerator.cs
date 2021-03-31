using UnityEngine;

public class CutterGenerator : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] AudioSource cutterSound;

    public void SpwanCutter(Vector3 position)
    {
        int random = Random.Range(1, 100);
        GameObject cutter = objectPooler.GetPooledGameObject();
        cutter.transform.position = new Vector3(position.x, position.y+1, 10);
        if (random < 90) return;
        float distanceBetweenPlayerAndCutter = player.transform.position.x - cutter.transform.position.x;
        cutter.SetActive(true);
       
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
