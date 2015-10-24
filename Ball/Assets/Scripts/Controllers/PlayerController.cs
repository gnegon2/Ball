using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private enum mode
    {
        normal,
        bigger,
        faster
    };

    public int lives;
    public int MAX_SCORE;
    public PhysicMaterial physicMaterialNormal;
    public PhysicMaterial physicMaterialBigger;
    public PhysicMaterial physicMaterialFaster;
    public GameObject Explosion;

    [System.Serializable]
    public class Texts
    {
        public Text livesText;
        public Text scoreText;
        public Text winText;
    }
    
    [System.Serializable]
    public class AudioClips
    {
        public AudioClip meat;
        public AudioClip change;
        public AudioClip pickUp;
        public AudioClip winner;
    }

    public AudioClips audioClips;
    public Texts texts;

    private float NORMAL_MASS = 1;
    private float NORMAL_SCALE = 1;
    private float NORMAL_SPEED = 8;
    private float BIGGER_MASS = 30;
    private float BIGGER_SCALE = 2;
    private float BIGGER_SPEED = 60;
    private float FASTER_MASS = 0.3f;
    private float FASTER_SCALE = 0.7f;
    private float FASTER_SPEED = 12;
    private float CHANGE_VOLUME = 0.1f;
    private float PICKUP_VOLUME = 0.1f;
    private float WINNER_VOLUME = 0.3f;
    private float MEAT_VOLUME = 0.2f;
    private float EFFECT_NORMAL = 1f;
    private float EFFECT_RAIN = 0.3f;
    private float STEAM_POWER = 40f;

    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private Collider playerCollider;
    private AudioSource audioSource;
    private int playerScore;
    private float volume;
    private Vector3 scaleNormal;
    private Vector3 scaleBigger;
    private Vector3 scaleFaster;
    private mode playerMode;
    private float playerSpeed;
    private float effectSpeed;

	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        playerScore = 0;
        SetLivesText();
        SetScoreText();
        playerMode = mode.normal;
        playerCollider.material = physicMaterialNormal;
        playerSpeed = NORMAL_SPEED;
        effectSpeed = EFFECT_NORMAL;
        scaleNormal = new Vector3(NORMAL_SCALE, NORMAL_SCALE, NORMAL_SCALE);
        scaleBigger = new Vector3(BIGGER_SCALE, BIGGER_SCALE, BIGGER_SCALE);
        scaleFaster = new Vector3(FASTER_SCALE, FASTER_SCALE, FASTER_SCALE);
    }

	void Update () {
	
	}

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        playerRigidbody.AddForce(movement * playerSpeed * effectSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            playerScore++;
            SetScoreText();
            if (playerScore == MAX_SCORE)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips.winner, WINNER_VOLUME);
                texts.winText.text = "You Win!";
            }
            else
            {
                audioSource.PlayOneShot(audioClips.pickUp, PICKUP_VOLUME); 
            }           
        }
        else if (other.gameObject.CompareTag("Meat"))
        {
            other.gameObject.SetActive(false);
            lives++;
            SetLivesText();
            audioSource.PlayOneShot(audioClips.meat, MEAT_VOLUME);
        }
        else if (other.gameObject.CompareTag("Normal"))
        {
            if (playerMode != mode.normal)
            {
                playerCollider.material = physicMaterialNormal;
                playerMode = mode.normal;
                playerSpeed = NORMAL_SPEED;
                playerTransform.localScale = scaleNormal;
                playerRigidbody.mass = NORMAL_MASS;
                audioSource.PlayOneShot(audioClips.change, CHANGE_VOLUME);
            }
        }
        else if (other.gameObject.CompareTag("Bigger"))
        {
            if (playerMode != mode.bigger)
            {
                playerCollider.material = physicMaterialBigger;
                playerMode = mode.bigger;
                playerSpeed = BIGGER_SPEED;
                playerTransform.localScale = scaleBigger;
                playerRigidbody.mass = BIGGER_MASS;
                audioSource.PlayOneShot(audioClips.change, CHANGE_VOLUME); 
            }
        }
        else if (other.gameObject.CompareTag("Faster"))
        {
            if (playerMode != mode.faster)
            {
                playerCollider.material = physicMaterialFaster;
                playerMode = mode.faster;
                playerSpeed = FASTER_SPEED;
                playerTransform.localScale = scaleFaster;
                playerRigidbody.mass = FASTER_MASS;
                audioSource.PlayOneShot(audioClips.change, CHANGE_VOLUME);
            }
        }
        else if (other.gameObject.CompareTag("Rain"))
        {
            effectSpeed = EFFECT_RAIN;
            playerRigidbody.velocity /= 3;
        }
        else if (other.gameObject.CompareTag("WallOfFire"))
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            LoseLife();                  
        }
        
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Steam"))
        {
            int x = other.gameObject.GetComponent<SteamDirection>().x;
            int y = other.gameObject.GetComponent<SteamDirection>().y;
            int z = other.gameObject.GetComponent<SteamDirection>().z;
            Vector3 steam = new Vector3(STEAM_POWER * x, STEAM_POWER * y, STEAM_POWER * z);
            playerRigidbody.AddForce(steam);   
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rain"))
        {
            effectSpeed = EFFECT_NORMAL;
        } 
    }

    void LoseLife()
    {
        lives--;
        SetLivesText();
        if (lives <= 0)
        {
            texts.winText.text = "You Lose!";
            Lose();
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(0, 1, 0);
        }
    }

    void Lose()
    {

    }

    void SetLivesText()
    {
        texts.livesText.text = "Lives: " + lives.ToString();  
    }

    void SetScoreText()
    {
        texts.scoreText.text = "Score: " + playerScore.ToString();        
    }
}
