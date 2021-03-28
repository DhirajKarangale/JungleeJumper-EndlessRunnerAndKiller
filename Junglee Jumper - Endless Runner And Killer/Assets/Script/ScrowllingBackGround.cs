using UnityEngine;

public class ScrowllingBackGround : MonoBehaviour
{
    [SerializeField] Renderer backGround;
    [SerializeField] float backGroundSpeed;

    private void Update()
    {
        backGround.material.mainTextureOffset += new Vector2(backGroundSpeed * Time.deltaTime, 0);
    }
}
