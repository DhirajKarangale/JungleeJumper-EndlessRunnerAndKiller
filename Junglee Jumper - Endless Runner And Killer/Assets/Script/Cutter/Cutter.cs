using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;
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
