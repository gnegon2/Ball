using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        switch(gameObject.tag)
        {
            case "Meat":
                transform.Rotate(new Vector3(0, 40, 0) * Time.deltaTime);
                break;
            case "Pick Up":
                transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
                break;
            case "Normal":
                transform.Rotate(new Vector3(-15, -45, 75) * Time.deltaTime);
                break;
            case "Bigger":
                transform.Rotate(new Vector3(25, 5, -15) * Time.deltaTime);
                break;
            case "Faster":
                transform.Rotate(new Vector3(95, 155, -135) * Time.deltaTime);
                break;
        }
        
	}
}
