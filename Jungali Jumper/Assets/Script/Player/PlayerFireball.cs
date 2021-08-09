using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] GameObject fireballExplosionEffect;
    [SerializeField] GameObject impactEffect;
    
    private Player player;
    private ScoreManager scoreManager;


    [SerializeField] GameObject cutterDestroyEffect;

    [SerializeField] GameObject zombieBloodSplash;
    [SerializeField] GameObject enemieDestriyEffect;
          
    [Header("Camera Shake")]
    private Vector3 cameraInitialPosition;
    private float shakeMagnetude = 0.22f, shakeTime = 0.2f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scoreManager = FindObjectOfType<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        if(GameDataVariable.dataVariables[3] == 2)
        {
            rigidBody.velocity = -transform.right * speed * player.speed;
        }
        else
        {
            rigidBody.velocity = transform.right * speed * player.speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation), 1f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Zombie")
        {
            ShakeIt();

            collision.gameObject.SetActive(false);
            Destroy(Instantiate(enemieDestriyEffect, transform.position, transform.rotation), 3f);
            Destroy(Instantiate(zombieBloodSplash, transform.position + new Vector3(0, -2, -1), transform.rotation), 5f);

            scoreManager.score += 10;
            scoreManager.scoreDescreaseText.color = Color.green;
            scoreManager.scoreDescreaseText.text = "+10";
            scoreManager.scoreDescreaseText.gameObject.SetActive(true);
            scoreManager.DesableText();

            Destroy(Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation), 1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "ZombieFireball")
        {
            ShakeIt();

            scoreManager.score += 20;
            scoreManager.scoreDescreaseText.color = Color.green;
            scoreManager.scoreDescreaseText.text = "+20";
            scoreManager.scoreDescreaseText.gameObject.SetActive(true);
            scoreManager.DesableText();

            Destroy(collision.gameObject);
            Destroy(Instantiate(fireballExplosionEffect, transform.position, transform.rotation), 2f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Cutter")
        {
           ShakeIt();

            collision.gameObject.SetActive(false);
            Destroy(Instantiate(cutterDestroyEffect, transform.position, transform.rotation), 2f);

            scoreManager.score -= 15;
            scoreManager.scoreDescreaseText.gameObject.SetActive(true);
            scoreManager.scoreDescreaseText.color = Color.red;
            scoreManager.scoreDescreaseText.text = "-15";
            scoreManager.DesableText();

            Destroy(Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation), 1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "VerC")
        {
            ShakeIt();

            collision.gameObject.SetActive(false);
            Destroy(Instantiate(cutterDestroyEffect, transform.position, transform.rotation), 2f);

            scoreManager.score -= 25;
            scoreManager.scoreDescreaseText.color = Color.red;
            scoreManager.scoreDescreaseText.text = "-25";
            scoreManager.scoreDescreaseText.gameObject.SetActive(true);
            scoreManager.DesableText();

            Destroy(Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation), 1);
            Destroy(gameObject);
        }
    }

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
