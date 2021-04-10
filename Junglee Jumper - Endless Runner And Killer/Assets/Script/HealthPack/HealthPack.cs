using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private Player player;
    [SerializeField] GameObject healthPackUsedEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

  private void OnTriggerEnter2D(Collider2D collision)
  {
      if(((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "PlayerFireball")) && (player.currentHealth < player.health))
      {
          player.currentHealth = player.health;
          GameObject currentHealthpackUsedEffect = Instantiate(healthPackUsedEffect,player.transform.position + new Vector3(0 ,-1,0), Quaternion.Euler(-90,0,0));
          Destroy(currentHealthpackUsedEffect,2f);
          gameObject.SetActive(false);
      }
  }
}
