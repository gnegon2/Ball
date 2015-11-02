using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTextScriptSecondPlayer : MonoBehaviour
{

    static public int score;

    static private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

	static public void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
