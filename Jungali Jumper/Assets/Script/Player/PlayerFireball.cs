using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private Player player;
    private AudioSource firballReleseSound;
    [SerializeField] GameObject fireballExplosionEffect;
    private Rigidbody2D rigidBody;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage;
    [SerializeField] float speed;
    public static bool twoFireballCollide, playerFireballHitObject, playerFireballCollideZombie, playerFireballCollideWithCutter, playerFireballCollideWithVerticalCutter;

    [Header("Camera Shake")]
    private Vector3 cameraInitialPosition;
    private float shakeMagnetude = 0.18f, shakeTime = 0.43f;
    private Camera mainCamera;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();
        firballReleseSound = GetComponent<AudioSource>();
        twoFireballCollide = false;
        if(GameDataVariable.dataVariables[3] == 2)
        {
            rigidBody.velocity = -transform.right * speed * player.speed;
        }
        else
        {
            rigidBody.velocity = transform.right * speed * player.speed;
        }
        firballReleseSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            firballReleseSound.Stop();
            playerFireballCollideWithCutter = false;
            playerFireballCollideWithVerticalCutter = false;
            playerFireballCollideZombie = false;
            playerFireballHitObject = true;
            twoFireballCollide = false;
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }

        else if (collision.gameObject.tag == "Zombie")
        {
            ShakeIt();
            firballReleseSound.Stop();
            playerFireballCollideZombie = true;
            playerFireballHitObject = true;
            twoFireballCollide = false;
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        else if (collision.gameObject.tag == "ZombieFireball")
        {
            ShakeIt();
            firballReleseSound.Stop();
            playerFireballHitObject = false;
            twoFireballCollide = true;
            Destroy(gameObject);
            GameObject currentFireballExplosionEffect = Instantiate(fireballExplosionEffect, transform.position, transform.rotation);
            Destroy(currentFireballExplosionEffect, 4f);
        }
        else if (collision.gameObject.tag == "Cutter")
        {
            ShakeIt();
            playerFireballCollideWithCutter = true;
            playerFireballCollideWithVerticalCutter = false;
            firballReleseSound.Stop();
            playerFireballHitObject = true;
            twoFireballCollide = false;
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        else if (collision.gameObject.tag == "VerC")
        {
            ShakeIt();
            playerFireballCollideWithVerticalCutter = true;
            playerFireballCollideWithCutter = false;
            firballReleseSound.Stop();
            playerFireballHitObject = true;
            twoFireballCollide = false;
            Destroy(gameObject);
            GameObject currentImpactEffect = Instantiate(impactEffect, transform.position + new Vector3(1, 0, 0), transform.rotation);
            Destroy(currentImpactEffect, 1f);
        }
        else
        {
            playerFireballHitObject = false;
            twoFireballCollide = false;
        }
    }

    private void ShakeIt()
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
