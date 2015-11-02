using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesTextScriptFirstPlayer : MonoBehaviour
{

    static public int lives;

    static private Text livesText;

    void Start()
    {
        livesText = GetComponent<Text>();
    }

    static public void SetLivesText()
    {
        livesText.text = "Lives: " + lives;
    }
}
