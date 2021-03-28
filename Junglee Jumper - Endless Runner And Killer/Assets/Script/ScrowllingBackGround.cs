using UnityEngine;

public class ScrowllingBackGround : MonoBehaviour
{
    [SerializeField] Renderer backGround;
    [SerializeField] Player player;
    [SerializeField] float backGroundSpeed;

    private void Update()
    {
        if(player.playerRuning)
        {
            backGround.material.mainTextureOffset += new Vector2(backGroundSpeed * Time.deltaTime, 0);
        }
    }
}
