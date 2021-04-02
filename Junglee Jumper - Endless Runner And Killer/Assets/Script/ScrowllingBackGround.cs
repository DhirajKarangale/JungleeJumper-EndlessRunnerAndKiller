using UnityEngine;

public class ScrowllingBackGround : MonoBehaviour
{
    [SerializeField] Renderer backGround;
    [SerializeField] Player player;
    public float backGroundSpeed;

    private void Update()
    {
        if(player.playerRuning && !player.isPlayerHitObstacles && !player.isPlayerDead)
        {
            backGround.material.mainTextureOffset += new Vector2(backGroundSpeed * Time.deltaTime, 0);
        }
    }
}
