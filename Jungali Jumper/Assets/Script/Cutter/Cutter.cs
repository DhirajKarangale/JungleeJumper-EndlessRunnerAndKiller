using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] AudioSource cutterSound;

    private void Start()
    {
        if(!Player.isPlayerDead && GameManager.isGameStart && !ScoreManager.isPause) cutterSound.Play();
    }

   private void Update()
   {
        if(Player.isPlayerDead || ScoreManager.isPause) cutterSound.Stop();
      
   }
}
