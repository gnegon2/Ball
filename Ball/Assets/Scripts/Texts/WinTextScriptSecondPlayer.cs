using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinTextScriptSecondPlayer : MonoBehaviour
{

    static private Text winText;

    void Start()
    {
        winText = GetComponent<Text>();
    }

    static public void SetWinText()
    {
        winText.text = "You win!";
    }

    static public void SetLoseText()
    {
        winText.text = "You lose!";
    }
}
