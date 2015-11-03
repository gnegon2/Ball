using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public int score;

    public GameObject firstPlayer;
    public GameObject secondPlayer;

    public GameObject firstPlayerRespawn;
    public GameObject secondPlayerRespawn;

    public GameObject mainCamera;
    public GameObject firstPlayerCamera;
    public GameObject secondPlayerCamera;

    public GameObject firstPlayerCanvas;
    public GameObject secondPlayerCanvas;

    public Text firstPlayerLivesText;
    public Text secondPlayerLivesText;

    private int remainingPlayers;

	// Use this for initialization
	void Start () {
        score = GameObject.FindGameObjectsWithTag("Score").Length;
        firstPlayerRespawn = GameObject.FindGameObjectWithTag("firstPlayerRespawn");
        secondPlayerRespawn = GameObject.FindGameObjectWithTag("secondPlayerRespawn");
        mainCamera = GameObject.FindGameObjectWithTag("mainCamera");
	}

    public void CreatePlayers(float multiplayerMode)
    {
        Debug.Log("Creating Players...");
        mainCamera.SetActive(false); // turn off main camera
        Debug.Log("mainCamera turned off.");

        firstPlayer = Instantiate(Resources.Load("Prefabs/GamePlay/player", typeof(GameObject))) as GameObject;
        firstPlayer.transform.position = firstPlayerRespawn.transform.position;
        firstPlayer.GetComponent<PlayerController>().playerNumber = 1;

        firstPlayerCamera = Instantiate(Resources.Load("Prefabs/GamePlay/playerCamera", typeof(GameObject))) as GameObject;
        firstPlayerCamera.GetComponent<CameraController>().player = firstPlayer;

        firstPlayerCanvas = Instantiate(Resources.Load("Prefabs/Texts/firstPlayerCanvas", typeof(GameObject))) as GameObject;
        firstPlayerLivesText = firstPlayerCanvas.transform.Find("livesText").GetComponent<Text>();

        Debug.Log("multiplayerMode = " + multiplayerMode);
        if (multiplayerMode == 1)
        {
            Debug.Log("multiplayerMode Off");
            firstPlayerCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1f, 1f);

            remainingPlayers = 1;
        }
        else if(multiplayerMode == 2)
        {
            Debug.Log("multiplayerMode On");

            secondPlayer = Instantiate(Resources.Load("Prefabs/GamePlay/player", typeof(GameObject))) as GameObject;
            secondPlayer.transform.position = secondPlayerRespawn.transform.position;
            secondPlayer.GetComponent<PlayerController>().playerNumber = 2;

            secondPlayerCamera = Instantiate(Resources.Load("Prefabs/GamePlay/playerCamera", typeof(GameObject))) as GameObject;
            secondPlayerCamera.GetComponent<CameraController>().player = secondPlayer;

            firstPlayerCamera.GetComponent<Camera>().rect = new Rect(0, .5f, 1f, .5f);
            secondPlayerCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1f, .5f);

            secondPlayerCanvas = Instantiate(Resources.Load("Prefabs/Texts/secondPlayerCanvas", typeof(GameObject))) as GameObject;
            secondPlayerLivesText = secondPlayerCanvas.transform.Find("livesText").GetComponent<Text>();

            remainingPlayers = 2;
        }  
    }

    public void PlayerLose(int playerNumber)
    {
        remainingPlayers--;
        
        if( remainingPlayers == 1 )
        {
            if(playerNumber == 1)
            {
                firstPlayerCamera.SetActive(false);
                firstPlayerCanvas.SetActive(false);
                secondPlayerCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1f, 1f);
                secondPlayerLivesText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                secondPlayerLivesText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            }
            else if(playerNumber == 2)
            {
                secondPlayerCamera.SetActive(false);
                secondPlayerCanvas.SetActive(false);
                firstPlayerCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1f, 1f);
                firstPlayerLivesText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                firstPlayerLivesText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            }
        }
        
        if(remainingPlayers == 0 )
        {
            LoseGame();
        }   
    }

    public void SetLivesText(int playerNumber, int lives)
    {
        if (playerNumber == 1)
            firstPlayerLivesText.text = "Lives: " + lives;
        else if(playerNumber == 2)
            secondPlayerLivesText.text = "Lives: " + lives;
    }

    public void LoseGame()
    {

    }

    public void WinGame()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}
}

