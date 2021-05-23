using UnityEngine;

public class GroundDestroyer : MonoBehaviour
{
    private GameObject groundEndPoint;

    private void Start()
    {
        groundEndPoint = GameObject.Find("GroundEndPoint");
    }

    private void Update()
    {
        if(transform.position.x<groundEndPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
