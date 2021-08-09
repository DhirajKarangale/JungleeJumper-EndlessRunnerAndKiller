using UnityEngine;

public class VerticalCutter : MonoBehaviour
{
    private Player player;
    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!Player.isPlayerDead && GameManager.isGameStart && !ScoreManager.isPause) cutterSound.Play();
    }

    private void Update()
    {
        if (Player.isPlayerDead || ScoreManager.isPause) cutterSound.Stop();
    }
}
