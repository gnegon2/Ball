using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody playerRigidbody;
    private int count;
	// Use this for initialization
	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        playerRigidbody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }      
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
            winText.text = "You Win!";
    }
}
