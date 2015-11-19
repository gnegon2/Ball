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
    public int playerNumber;
    public PhysicMaterial physicMaterialNormal;
    public PhysicMaterial physicMaterialBigger;
    public PhysicMaterial physicMaterialFaster;
    public GameObject Explosion;
    
    [System.Serializable]
    public class AudioClips
    {
        public AudioClip meat;
        public AudioClip change;
        public AudioClip pickUp;
        public AudioClip winner;
    }

    public AudioClips audioClips;

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
    private float volume;
    private Vector3 scaleNormal;
    private Vector3 scaleBigger;
    private Vector3 scaleFaster;
    private mode playerMode;
    private float playerSpeed;
    private float effectSpeed;

    private GameLogic GameLogic;
    private bool isOnGround;

    void Start ()
    {
        GameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        GameLogic.SetLivesText(playerNumber, lives);

        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        
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
        float moveHorizontal;
        float moveVertical;

        if( playerNumber == 1 )
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump") && isOnGround)
                playerRigidbody.velocity += new Vector3(0, 7, 0);
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = Input.GetAxis("Vertical2");
            if (Input.GetButtonDown("Jump2") && isOnGround)
                playerRigidbody.velocity += new Vector3(0, 7, 0);
        }

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        playerRigidbody.AddForce(movement * playerSpeed * effectSpeed);
      
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            other.gameObject.SetActive(false);
            GameLogic.score--;
            if (GameLogic.score == 0)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips.winner, WINNER_VOLUME);
                GameLogic.firstPlayerInfoText.text = "Win!";
                if(GameLogic.isMultiplayer)
                    GameLogic.secondPlayerInfoText.text = "Win!";
                Invoke("winMap", audioClips.winner.length);    
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
            GameLogic.SetLivesText(playerNumber, lives);
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
        else if (other.gameObject.CompareTag("Portal"))
        {
            transform.position = other.gameObject.GetComponent<Portal>().destination.transform.position;
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            if (playerNumber == 1)
            {
                if (GameLogic.firstPlayerLastCheckpoint != other.gameObject)
                {
                    GameLogic.firstPlayerLastCheckpoint = other.gameObject;
                    GameLogic.firstPlayerInfoText.text = "Checkpoint!";
                    Invoke("cleanInfoText", 3);  
                }            
            }
            else
            {
                if (GameLogic.secondPlayerLastCheckpoint != other.gameObject)
                {
                    GameLogic.secondPlayerInfoText.text = "Checkpoint!";
                    GameLogic.secondPlayerLastCheckpoint = other.gameObject;
                    Invoke("cleanInfoText", 3); 
                }   
            }
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
        GameLogic.SetLivesText(playerNumber, lives);
        if (lives <= 0)
        {
            this.gameObject.SetActive(false);
            GameLogic.PlayerLose(playerNumber);
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            if (playerNumber == 1)
            {
                transform.position = GameLogic.firstPlayerLastCheckpoint.transform.position;
            }
            else
            {
                transform.position = GameLogic.secondPlayerLastCheckpoint.transform.position;
            }      
        }
    }

    void cleanInfoText()
    {
        if (playerNumber == 1)
        {
            GameLogic.firstPlayerInfoText.text = "";
        }
        else
        {
            GameLogic.secondPlayerInfoText.text = "";
        }
    }

    void winMap()
    {
        GameLogic.WinMap();
    }
}
