using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private AudioSource coinPickSound;
    public static bool isCoinHit;
   
   

    private void Start()
    {
        coinPickSound = GameObject.Find("CoinPickSound").GetComponent<AudioSource>();
        isCoinHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (coinPickSound.isPlaying) coinPickSound.Stop();
            coinPickSound.Play();
            gameObject.SetActive(false);
            isCoinHit = true;
        }
        else
        {
            isCoinHit = false;
        }
    }
}
