using UnityEngine;
using System.Collections;

public class SetMultiplayerMode : MonoBehaviour {

    public GameObject UI;

    private StartOptions startOptions;

    //Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
    public void SetMultiplayer(float multiMode)
    {
        Debug.Log("multiMode = " + multiMode);
        startOptions = UI.GetComponent<StartOptions>();
        startOptions.multiplayerMode = multiMode;
    }
}
