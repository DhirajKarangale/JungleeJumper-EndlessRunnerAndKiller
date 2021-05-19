using UnityEngine;

public class Cutter : MonoBehaviour
{
    private Player player;
    private ScoreManager scoreManager;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
         scoreManager = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(!Player.isPlayerDead && GameManager.isGameStart && !ScoreManager.isPause) cutterSound.Play();
    }

   private void Update()
   {
        if(Player.isPlayerDead || ScoreManager.isPause) cutterSound.Stop();
       if(PlayerFireball.playerFireballCollideWithCutter)
       {
          PlayerFireball.playerFireballCollideWithCutter = false;
          scoreManager.score -= 3;
          gameObject.SetActive(false);
          GameObject currentDestryEffect = Instantiate(destroyEffect,transform.position,transform.rotation);
          Destroy(currentDestryEffect,2f);
       }
   }
}
