using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public bool isPlayerCamera;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        if (isPlayerCamera)
            offset = player.transform.position + new Vector3(0,10,-10);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
