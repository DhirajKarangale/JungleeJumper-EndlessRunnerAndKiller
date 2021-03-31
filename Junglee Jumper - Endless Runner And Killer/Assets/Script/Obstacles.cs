using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] AudioSource obstaclesSound;

    private void Update()
    {
        if(!Player.isPlayerDead)
        {
            obstaclesSound.Play();
        }
        else
        {
            obstaclesSound.Stop();
        }
    }
}
