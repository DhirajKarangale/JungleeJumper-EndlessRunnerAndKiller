using UnityEngine;

public class Cutter : MonoBehaviour
{
    private Player player;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(!cutterSound.isPlaying && !player.isPlayerDead && GameManager.isGameStart && !ScoreManager.isPause) cutterSound.Play();
    }

   private void Update()
   {
        if(player.isPlayerDead || ScoreManager.isPause) cutterSound.Stop();
       if(PlayerFireball.playerFireballCollideWithCutter)
       {
          PlayerFireball.playerFireballCollideWithCutter = false;
          gameObject.SetActive(false);
          GameObject currentDestryEffect = Instantiate(destroyEffect,transform.position,transform.rotation);
          Destroy(currentDestryEffect,2f);
       }
   }
}
