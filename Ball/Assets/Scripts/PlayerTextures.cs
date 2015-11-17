using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTextures : MonoBehaviour {

    public Toggle splitMetalBallToggle;
    public Toggle wheelBallToggle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool checkToggle(string name)
    {
        if(name == "splitMetalBallToggle")
        {
            return splitMetalBallToggle.isOn;
        }
        else if(name == "wheelBallToggle")
        {
            return wheelBallToggle.isOn;
        }
        return false;
    }
}
