using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private AudioSource coinPickSound;
    private ScoreManager scoreManager;
    private float coinPickPoints = 15f;

    private void Start()
    {
        coinPickSound = GameObject.Find("CoinPickSound").GetComponent<AudioSource>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (coinPickSound.isPlaying) coinPickSound.Stop();
            coinPickSound.Play();
            gameObject.SetActive(false);
            scoreManager.score += coinPickPoints;
        }
    }
}
