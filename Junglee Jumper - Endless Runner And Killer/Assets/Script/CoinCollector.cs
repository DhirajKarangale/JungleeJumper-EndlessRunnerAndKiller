using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private AudioSource coinPickSound;
    [SerializeField] GameObject coinCollectEffect;
    public static Transform coinPosition;
    public static bool isCoinHit;
    private Rigidbody2D rigidbody;
    private float timeStamp;
    private bool goToPlayer;
    private GameObject player;
    private Vector2 playerDirection;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        coinPickSound = GameObject.Find("CoinPickSound").GetComponent<AudioSource>();
        isCoinHit = false;
    }

    private void Update()
    {
        if (goToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rigidbody.velocity = new Vector2(playerDirection.x, playerDirection.y) * 15f * (Time.time / timeStamp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            coinPosition = transform;
            if (coinPickSound.isPlaying) coinPickSound.Stop();
            coinPickSound.Play();
            gameObject.SetActive(false);
            Destroy(Instantiate(coinCollectEffect, this.gameObject.transform.position,Quaternion.identity), 0.1f);
            isCoinHit = true;
            goToPlayer = false;
        }
        else if((collision.gameObject.tag == "PlayerFireball") && (GameDataVariable.dataVariables[3] == 2))
        {
            coinPosition = transform;
            if (coinPickSound.isPlaying) coinPickSound.Stop();
            coinPickSound.Play();
            gameObject.SetActive(false);
            Destroy(Instantiate(coinCollectEffect, this.gameObject.transform.position, Quaternion.identity), 0.1f);
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
