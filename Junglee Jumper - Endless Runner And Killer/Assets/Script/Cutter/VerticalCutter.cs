using UnityEngine;

public class VerticalCutter : MonoBehaviour
{
    private Player player;
    private ScoreManager scoreManager;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!cutterSound.isPlaying && !player.isPlayerDead && GameManager.isGameStart && !ScoreManager.isPause) cutterSound.Play();
    }

    private void Update()
    {
        if (player.isPlayerDead || ScoreManager.isPause) cutterSound.Stop();
        if (PlayerFireball.playerFireballCollideWithVerticalCutter)
        {
            scoreManager.score -= 7;
            gameObject.SetActive(false);
            GameObject currentDestryEffect = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(currentDestryEffect, 2f);
            PlayerFireball.playerFireballCollideWithVerticalCutter = false;
        }
    }
}
