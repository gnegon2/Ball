using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
	
	// Update is called once per frame
	void LateUpdate () {
        if (player != null)
            transform.position = player.transform.position + new Vector3(0, 10, -10);
	}
}
