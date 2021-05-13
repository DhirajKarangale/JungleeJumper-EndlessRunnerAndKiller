using UnityEngine;

public class ScrowllingBackGround : MonoBehaviour
{
    private Renderer backGround;
    private Player player;
    public float backGroundSpeed;

    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       backGround = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(player.playerRuning && !player.isPlayerHitObstacles && !Player.isPlayerDead)
        {
            backGround.material.mainTextureOffset += new Vector2(backGroundSpeed * Time.deltaTime, 0);
        }
    }
}
