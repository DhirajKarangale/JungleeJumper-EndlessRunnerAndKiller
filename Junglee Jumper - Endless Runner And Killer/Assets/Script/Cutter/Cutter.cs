using UnityEngine;

public class Cutter : MonoBehaviour
{
    private Player player;
    [SerializeField] GameObject destroyEffect;

    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(!cutterSound.isPlaying) cutterSound.Play();
        if(player.isPlayerDead) cutterSound.Stop();
    }

   private void Update()
   {
       if(PlayerFireball.playerFireballCollideWithCutter)
       {
          PlayerFireball.playerFireballCollideWithCutter = false;
          gameObject.SetActive(false);
          GameObject currentDestryEffect = Instantiate(destroyEffect,transform.position,transform.rotation);
          Destroy(currentDestryEffect,2f);
       }
   }
}
