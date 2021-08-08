using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] GameObject coinCollectEffect;
    public static Transform coinPosition;
    public static bool isCoinHit;
    private Rigidbody2D rigidBody;
    private float timeStamp;
    private bool goToPlayer;
    private GameObject player;
    private Vector2 playerDirection;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isCoinHit = false;
    }

    private void Update()
    {
        if (goToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rigidBody.velocity = new Vector2(playerDirection.x, playerDirection.y) * 15f * (Time.time / timeStamp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            coinPosition = transform;
            gameObject.SetActive(false);
            Destroy(Instantiate(coinCollectEffect, this.gameObject.transform.position,Quaternion.identity), 0.4f);
            isCoinHit = true;
            goToPlayer = false;
        }
        else if((collision.gameObject.tag == "PlayerFireball") && (GameDataVariable.dataVariables[3] == 2))
        {
            coinPosition = transform;
            gameObject.SetActive(false);
            Destroy(Instantiate(coinCollectEffect, this.gameObject.transform.position, Quaternion.identity), 0.4f);
            isCoinHit = true;
            goToPlayer = false;
        }
        else if((collision.gameObject.name == "CoinMagnet") && !Player.isPlayerDead && (GameDataVariable.dataVariables[10] == 1))
        {
            timeStamp = Time.time;
            player = GameObject.Find("Player");
            goToPlayer = true;
        }
        else
        {
            isCoinHit = false;
            goToPlayer = false;
        }
    }
}
