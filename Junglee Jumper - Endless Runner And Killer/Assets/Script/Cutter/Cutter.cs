using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;
   private void Update()
   {
       if(PlayerFireball.playerFireballCollideWithCutter)
       {
          PlayerFireball.playerFireballCollideWithCutter = false;
          GameObject currentDestryEffect = Instantiate(destroyEffect,transform.position,transform.rotation);
          gameObject.SetActive(false);
          Destroy(currentDestryEffect,2f);
       }
   }
}
